using System.Reflection;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class EmploymentApplicationFactory
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private Entity? _newApplication;
        private EmploymentApplication? _dto = null;
        private readonly CoreJobPositions _coreJobPositionsQuery;
        private readonly JobOpportunities _jobOpportunityQuery;
        private EntityReference? _coreJobPositionRef = null;
        public EmploymentApplicationFactory(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            CoreJobPositions coreJobPositionsQuery,
            JobOpportunities jobOpportunityQuery
        )
        {
            _logger = loggerFactory.Create<EmploymentApplicationFactory>();
            _context = context;
            _coreJobPositionsQuery = coreJobPositionsQuery;
            _jobOpportunityQuery = jobOpportunityQuery;
        }
        public Guid Create(EmploymentApplication application)
        {
            // add complex fields
            _newApplication = new Entity(EntityLogicalNames.EmploymentApplication);
            _dto = application;

            if (_dto == null)
            {
                _logger.WriteLog("Unable to resolve EmploymentApplication DTO.");
                throw new Exception();
            }

            try
            {
                // test VisaWorkApprovalDetails
                _logger.WriteLog("Before vis/work approval details mapping");
                var visaStatus = _dto.VisaWorkApprovalDetails?.ToLowerInvariant();
                _newApplication[EmploymentApplicationFields.IsPermanentResident] = visaStatus == "permanentresident";
                _newApplication[EmploymentApplicationFields.HasWorkVisa] = visaStatus == "workvisa";

                // test preferred name
                _logger.WriteLog("Before preferred name mapping");

                _newApplication[EmploymentApplicationFields.PreferredName] = string.IsNullOrWhiteSpace(_dto.PreferredName)
                    ? (_dto.GivenNames ?? "").Split(' ')[0]
                    : _dto.PreferredName;

                // test CPR and First Aid qualifications
                _logger.WriteLog("Before cpr and first aid mapping");
                _newApplication[EmploymentApplicationFields.CurrentFirstAidYN] = string.IsNullOrWhiteSpace(_dto.NameOfFirstAidQualification) == false;
                _newApplication[EmploymentApplicationFields.CPRQualificationYN] = string.IsNullOrWhiteSpace(_dto.NameOfCPRQualification) == false;

                var properties = typeof(EmploymentApplication).GetProperties();

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<ApplicationFieldAttribute>();

                    if (attribute == null) continue;
                    if (excludedFields.Contains(attribute.FieldName)) continue;
                    var value = property.GetValue(_dto);

                    _logger.WriteLog(
                        "Mapping {0} -> {1}, Value: {2}",
                        property.Name,
                        attribute.FieldName,
                        value ?? "<null>"
                    );

                    if (value == null) continue;

                    var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    if (type.IsEnum)
                    {
                        _newApplication[attribute.FieldName] = new OptionSetValue(Convert.ToInt32(value));
                    }
                    else
                    {
                        _newApplication[attribute.FieldName] = value;
                    }
                }

                BuildAddresses();
                MapJobLookUps(_dto.WebFormOptionValue);

                if (_coreJobPositionRef == null) throw new InvalidPluginExecutionException("CoreJobPositionRef is null. Exiting.");

                MapApplicationType(_coreJobPositionRef.Id);
                _newApplication[EmploymentApplicationFields.SubmissionDate] = DateTime.UtcNow;

                return _context.Runtime.UserService.Create(_newApplication);
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Encountered exception in EmploymentApplicationFactory, ex: {0}", ex);
                throw;
            }
        }
        private void MapApplicationType(Guid coreJobPositionId)
        {
            _logger.WriteLog("Entered MapApplicationType.");

            ApplicationType applicationType = ApplicationType.Other;

            string[] attributes = [CoreJobPositionFields.CmeaType, CoreJobPositionFields.PAOSSubType];

            if (_newApplication == null)
            {
                _logger.WriteLog("Unable to resolve _newApplication.");
                throw new InvalidPluginExecutionException();
            }

            _logger.WriteLog("Retrieving CoreJobPosition properties in MapApplicationType.");
            var entity = _coreJobPositionsQuery.GetPropertyValues(coreJobPositionId, attributes) ?? throw new InvalidPluginExecutionException($"Core Job Position {coreJobPositionId} not found.");

            _logger.WriteLog("Setting cmeaTypeValue.");
            var cmeaTypeValue = entity.GetAttributeValue<OptionSetValue>(CoreJobPositionFields.CmeaType) ?? throw new InvalidPluginExecutionException("Core Job Position has no CMEA Type. Unable to set nullable field in new Employment Application Record. Exiting.");

            _logger.WriteLog("Casting cmeaTypeValue to CMEAType enum and storing as cmeaType");
            var cmeaType = (CMEAType)cmeaTypeValue.Value;

            if (cmeaType == CMEAType.Teaching)
            {
                applicationType = ApplicationType.Teaching;
            }
            else if (cmeaType == CMEAType.PAOS)
            {
                _logger.WriteLog("Setting paosSubTypeValue.");
                var paosSubTypeValue = entity.GetAttributeValue<OptionSetValue>(CoreJobPositionFields.PAOSSubType) ?? throw new InvalidPluginExecutionException("CMEA Type is PAOS but CoreJobPosition PAOS Sub-Type is null. Exiting. ");

                _logger.WriteLog("Casting paosSubTypeValue to PAOSSubType enum and storing as paosSubType");
                var paosSubType = (PAOSSubType)paosSubTypeValue.Value;

                if (paosSubType == PAOSSubType.CoCurricular)
                {
                    applicationType = ApplicationType.CoCurricular;
                }
                else if (paosSubType == PAOSSubType.NonCoCurricular)
                {
                    applicationType = ApplicationType.Support;
                }
            }

            _logger.WriteLog("Setting _newApplication ApplicationType and CMEAType properties.");
            _newApplication[EmploymentApplicationFields.ApplicationType] = MapOptionSetValue((int)applicationType);
            _newApplication[EmploymentApplicationFields.CMEAType] = MapOptionSetValue((int)cmeaType);
            _logger.WriteLog("Returning to calling factory instance.");
        }
        private void MapJobLookUps(int webFormOptionValue)
        {
            // query and set entity reference (parent job opp.)
            var entity = _jobOpportunityQuery.GetEntityByCode(webFormOptionValue);

            if (entity == null) throw new InvalidPluginExecutionException("Job Opportunity entity returned to MapJobLookups in EmploymentApplicationFactory is null. Exiting.");

            if (_newApplication == null)
            {
                _logger.WriteLog("Unable to resolve _newApplication.");
                throw new InvalidPluginExecutionException();
            }

            _newApplication[EmploymentApplicationFields.ParentJobOpportunity] = entity.ToEntityReference();

            _logger.WriteLog("Retrieving CoreJobPositionRef");
            _coreJobPositionRef = entity.GetAttributeValue<EntityReference>(JobOpportunityFields.HiringFor);

            if (_coreJobPositionRef == null) throw new InvalidPluginExecutionException("Unable to resolve CoreJobPosition. Exiting.");

            _newApplication[EmploymentApplicationFields.CoreJobPosition] = _coreJobPositionRef;

            _logger.WriteLog("MapJobLookups completed.");
        }
        private void BuildAddresses()
        {
            _logger.WriteLog("Entered EmploymentApplicationFactory.BuildAddresses()");

            if (_newApplication == null)
            {
                _logger.WriteLog("Unable to resolve _newApplication.");
                throw new InvalidPluginExecutionException();
            }

            if (_dto == null)
            {
                _logger.WriteLog("Unable to resolve _dto.");
                throw new InvalidPluginExecutionException();
            }

            _logger.WriteLog("Building Address");
            if (_dto.AddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.AddressLine1}</p>
                <p>{(_dto.Suburb ?? "NULL").ToUpper()} {_dto.State} {_dto.Postcode}</p>
                <p>{_dto.Country}</p>";

                _newApplication[EmploymentApplicationFields.Address] = address;
            }

            _logger.WriteLog("Building ChurchAddress");
            if (_dto.AddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.AddressLine1}</p>
                <p>{(_dto.Suburb ?? "NULL").ToUpper()} {_dto.State} {_dto.Postcode}</p>
                <p>{_dto.Country}</p>";

                _newApplication[EmploymentApplicationFields.Address] = address;
            }

            _logger.WriteLog("Building ChurchAddress");
            if (_dto.ChurchAddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.ChurchAddressLine1}</p>
                <p>{(_dto.ChurchSuburb ?? "NULL").ToUpper()} {_dto.ChurchState} {_dto.ChurchPostcode}</p>
                <p>{_dto.ChurchCountry}</p>";

                _newApplication[EmploymentApplicationFields.ChurchAddress] = address;
            }

            _logger.WriteLog("Building CurrentEmployerAddress");
            if (_dto.CurrentEmployerAddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.CurrentEmployerAddressLine1}</p>
                <p>{(_dto.CurrentEmployerSuburb ?? "NULL").ToUpper()} {_dto.CurrentEmployerState} {_dto.CurrentEmployerPostcode}</p>
                <p>{_dto.CurrentEmployerCountry}</p>";

                _newApplication[EmploymentApplicationFields.CurrentEmployerAddress] = address;
            }

            _logger.WriteLog("Building Referee1Address");
            if (_dto.Referee1AddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.Referee1AddressLine1}</p>
                <p>{(_dto.Referee1Suburb ?? "NULL").ToUpper()} {_dto.Referee1State} {_dto.Referee1Postcode}</p>
                <p>{_dto.Referee1Country}</p>";

                _newApplication[EmploymentApplicationFields.Referee1Address] = address;
            }

            _logger.WriteLog("Building Referee2Address");
            if (_dto.Referee2AddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.Referee2AddressLine1}</p>
                <p>{(_dto.Referee2Suburb ?? "NULL").ToUpper()} {_dto.Referee2State} {_dto.Referee2Postcode}</p>
                <p>{_dto.Referee2Country}</p>";

                _newApplication[EmploymentApplicationFields.Referee2Address] = address;
            }

            _logger.WriteLog("Building Referee3Address");
            if (_dto.Referee3AddressLine1 != null)
            {
                var address = $@"
                <p>{_dto.Referee3AddressLine1}</p>
                <p>{(_dto.Referee3Suburb ?? "NULL").ToUpper()} {_dto.Referee3State} {_dto.Referee3Postcode}</p>
                <p>{_dto.Referee1Country}</p>";

                _newApplication[EmploymentApplicationFields.Referee3Address] = address;
            }

            _logger.WriteLog("Returning to EmploymentApplicationFactory.Create caller.");
        }
        private static OptionSetValue MapOptionSetValue(int key)
        {
            return new OptionSetValue(key);
        }
        private readonly HashSet<string> excludedFields = new HashSet<string>()
        {
            EmploymentApplicationFields.IsPermanentResident,
            EmploymentApplicationFields.HasWorkVisa,
            EmploymentApplicationFields.CPRQualificationYN,
            EmploymentApplicationFields.CurrentFirstAidYN,
            EmploymentApplicationFields.ApplicationType,
            EmploymentApplicationFields.CMEAType,
            "pgrowth_webformoptionvalue"
        };
    }
}