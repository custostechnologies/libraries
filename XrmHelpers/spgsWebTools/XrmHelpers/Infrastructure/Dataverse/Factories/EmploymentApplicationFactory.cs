using System.Dynamic;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;
//using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Http;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class EmploymentApplicationFactory
    {
        private readonly ITracingService _tracer;
        private readonly IOrganizationService _service;
        private Entity _newApplication;
        private EmploymentApplication _dto = null;
        private CoreJobPositions _coreJobPositionsQuery = null;
        private EntityReference? _coreJobPositionRef = null;
        public EmploymentApplicationFactory(
            ITracingService tracer,
            IOrganizationService service
        )
        {
            _tracer = tracer;
            _service = service;
        }
        public Guid Create(EmploymentApplication application)
        {
            // add complex fields
            _newApplication = new Entity(EntityLogicalNames.EmploymentApplication);
            _dto = application;

            _coreJobPositionsQuery = new CoreJobPositions(_service, _tracer);

            try
            {
                // test VisaWorkApprovalDetails
                _tracer.Trace("Before vis/work approval details mapping");
                _newApplication[EmploymentApplicationFields.IsPermanentResident] = _dto.VisaWorkApprovalDetails.ToLower() == "permanentresident";
                _newApplication[EmploymentApplicationFields.HasWorkVisa] = _dto.VisaWorkApprovalDetails.ToLower() == "workvisa";

                // test preferred name
                _tracer.Trace("Before preferred name mapping");
                if (string.IsNullOrWhiteSpace(_dto.PreferredName))
                {
                    _newApplication[EmploymentApplicationFields.PreferredName] = _dto.GivenNames.Split(' ')[0];
                }
                else
                {
                    _newApplication[EmploymentApplicationFields.PreferredName] = _dto.PreferredName;
                }

                // test CPR and First Aid qualifications
                _tracer.Trace("Before cpr and first aid mapping");
                _newApplication[EmploymentApplicationFields.CurrentFirstAidYN] = string.IsNullOrWhiteSpace(_dto.NameOfFirstAidQualification) == false;
                _newApplication[EmploymentApplicationFields.CPRQualificationYN] = string.IsNullOrWhiteSpace(_dto.NameOfCPRQualification) == false;

                // set OptionSetValues from integer keys
                _tracer.Trace("Before Salutation mapping");
                _newApplication[EmploymentApplicationFields.Salutation] = MapOptionSetValue((int)_dto.Salutation);

                _tracer.Trace("Before State mapping");
                _newApplication[EmploymentApplicationFields.State] = MapOptionSetValue((int)_dto.State);

                _tracer.Trace("Before CurrentEmployerState mapping");
                _newApplication[EmploymentApplicationFields.CurrentEmployerState] = MapOptionSetValue((int)_dto.CurrentEmployerState);

                _tracer.Trace("Before Denomination mapping");
                _newApplication[EmploymentApplicationFields.Denomination] = MapOptionSetValue((int)_dto.Denomination);

                Type type = typeof(EmploymentApplicationFields);
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var field in fields)
                {
                    if (field.IsLiteral && !field.IsInitOnly)
                    {
                        string name = field.Name;
                        object value = field.GetValue(null);

                        Console.WriteLine($"Setting field {name} = {value}");
                        _newApplication[name] = value;
                    }
                }

                MapJobLookUps(_dto.WebFormOptionValue);
                MapApplicationType(_coreJobPositionRef.Id);

                return _service.Create(_newApplication);
            }
            catch (Exception ex)
            {
                _tracer.Trace("Encountered exception in EmploymentApplicationFactory, ex: {0}", ex);
                throw;
            }
        }
        private void MapApplicationType(Guid coreJobPositionId)
        {
            _tracer.Trace("Entered MapApplicationType.");

            ApplicationType applicationType = ApplicationType.Other;
            CMEAType cmeaType = CMEAType.NotApplicable;

            string[] attributes = [CoreJobPositionFields.CmeaType, CoreJobPositionFields.PAOSSubType];

            _tracer.Trace("Retrieving CoreJobPosition properties in MapApplicationType.");
            var entity = _coreJobPositionsQuery.GetPropertyValues(coreJobPositionId, attributes) ?? throw new InvalidPluginExecutionException($"Core Job Position {coreJobPositionId} not found.");

            _tracer.Trace("Setting cmeaTypeValue.");
            var cmeaTypeValue = entity.GetAttributeValue<OptionSetValue>(CoreJobPositionFields.CmeaType) ?? throw new InvalidPluginExecutionException("Core Job Position has no CMEA Type. Unable to set nullable field in new Employment Application Record. Exiting.");

            _tracer.Trace("Casting cmeaTypeValue to CMEAType enum and storing as cmeaType");
            cmeaType = (CMEAType)cmeaTypeValue.Value;

            if (cmeaType == CMEAType.Teaching)
            {
                applicationType = ApplicationType.Teaching;
            }
            else if (cmeaType == CMEAType.PAOS)
            {
                _tracer.Trace("Setting paosSubTypeValue.");
                var paosSubTypeValue = entity.GetAttributeValue<OptionSetValue>(CoreJobPositionFields.PAOSSubType) ?? throw new InvalidPluginExecutionException("CMEA Type is PAOS but CoreJobPosition PAOS Sub-Type is null. Exiting. ");

                _tracer.Trace("Casting paosSubTypeValue to PAOSSubType enum and storing as paosSubType");
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

            _tracer.Trace("Setting _newApplication ApplicationType and CMEAType properties.");
            _newApplication[EmploymentApplicationFields.ApplicationType] = MapOptionSetValue((int)applicationType);
            _newApplication[EmploymentApplicationFields.CMEAType] = MapOptionSetValue((int)cmeaType);
            _tracer.Trace("Returning to calling factory instance.");
        }
        private void MapJobLookUps(int webFormOptionValue)
        {
            // query and set entity reference (parent job opp.)
            var _jobOpportunityQuery = new JobOpportunities(_service, _tracer);
            var entity = _jobOpportunityQuery.GetEntityByCode(webFormOptionValue);

            if (entity == null) throw new InvalidPluginExecutionException("Job Opportunity entity returned to MapJobLookups in EmploymentApplicationFactory is null. Exiting.");

            _newApplication[EmploymentApplicationFields.ParentJobOpportunity] = entity.ToEntityReference();

            _tracer.Trace("Retrieving CoreJobPositionRef");
            _coreJobPositionRef = entity.GetAttributeValue<EntityReference>(JobOpportunityFields.HiringFor);

            if (_coreJobPositionRef == null) throw new InvalidPluginExecutionException("Unable to resolve CoreJobPosition. Exiting.");

            _newApplication[EmploymentApplicationFields.CoreJobPosition] = _coreJobPositionRef;

            _tracer.Trace("MapJobLookups completed.");
        }
        private static OptionSetValue MapOptionSetValue(int key)
        {
            return new OptionSetValue(key);
        }
    }
}