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
                // test numeric postcodes
                if (
                    !string.IsNullOrWhiteSpace(_dto.PostCodeText)
                        && _dto.PostCodeText.All(char.IsDigit)
                )
                {
                    _newApplication[EmploymentApplicationFields.PostCode] = int.Parse(_dto.PostCodeText);
                }
                if (
                    _dto.CurrentEmployerPostCode != null
                        && _dto.CurrentEmployerPostCode.All(char.IsDigit)
                )
                {
                    _newApplication[EmploymentApplicationFields.PostCode] = int.Parse(_dto.PostCodeText);
                }

                // test VisaWorkApprovalDetails
                _newApplication[EmploymentApplicationFields.IsPermanentResident] = _dto.VisaWorkApprovalDetails.ToLower() == "permanentresident";
                _newApplication[EmploymentApplicationFields.HasWorkVisa] = _dto.VisaWorkApprovalDetails.ToLower() == "workvisa";

                // test preferred name
                if (string.IsNullOrWhiteSpace(_dto.PreferredName))
                {
                    _newApplication[EmploymentApplicationFields.PreferredName] = _dto.GivenNames.Split(' ')[0];
                }
                else
                {
                    _newApplication[EmploymentApplicationFields.PreferredName] = _dto.PreferredName;
                }

                // test CPR and First Aid qualifications
                _newApplication[EmploymentApplicationFields.CurrentFirstAidYN] = string.IsNullOrWhiteSpace(_dto.NameOfFirstAidQualification) == false;
                _newApplication[EmploymentApplicationFields.CPRQualificationYN] = string.IsNullOrWhiteSpace(_dto.NameOfCPRQualification) == false;

                // set OptionSetValues from integer keys
                _newApplication[EmploymentApplicationFields.Salutation] = MapOptionSetValue((int)_dto.Salutation);
                _newApplication[EmploymentApplicationFields.CMEAType] = MapOptionSetValue((int)_dto.CMEAType);
                _newApplication[EmploymentApplicationFields.State] = MapOptionSetValue((int)_dto.State);
                _newApplication[EmploymentApplicationFields.CurrentEmployerState] = MapOptionSetValue((int)_dto.CurrentEmployerState);
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

                MapJobLookUps(_dto.JobOpportunityCode);
                MapApplicationType(_dto.JobPosition.Id);

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
            ApplicationType applicationType = ApplicationType.Other;
            CMEAType cmeaType = CMEAType.NotApplicable;

            var attributes = [CoreJobPositionFields.CMEAType, CoreJobPositionFields.PAOSSubType];
            var entity = _coreJobPositionsQuery.GetAttributeValues(coreJobPositionId, attributes) ?? throw new InvalidPluginExecutionException($"Core Job Position {coreJobPositionId} not found.");

            var cmeaTypeValue = entity.GetAttributeValue<MapOptionSetValue>(CoreJobPositionFields.CMEAType) ?? throw new InvalidPluginExecutionException("Core Job Position has no CMEA Type. Unable to set nullable field in new Employment Application Record. Exiting.");

            cmeaType = (CMEAType)cmeaTypeValue.Value;

            if (cmeaType == CMEAType.Teaching)
            {
                applicationType = ApplicationType.Teaching;
            }
            else if (cmeaType == CMEAType.PAOS)
            {
                var paosSubTypeValue = entity.GetAttributeValue<MapOptionSetValue>(CoreJobPositionFields.PAOSSubType) ?? throw new InvalidPluginExecutionException("CMEA Type is PAOS but CoreJobPosition PAOS Sub-Type is null. Exiting. ");

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

            _newApplication[EmploymentApplicationFields.Type] = MapOptionSetValue((int)applicationType);
            _newApplication[EmploymentApplicationFields.CMEAType] = MapOptionSetValue((int)cmeaType);
        }
        private void MapJobLookUps(int opportunityCode)
        {
            // query and set entity reference (parent job opp.)
            var _jobOpportunityQuery = new JobOpportunities(_service, _tracer);
            var entity = _jobOpportunityQuery.GetEntityByCode(_dto.JobOpportunityCode);
            _newApplication[EmploymentApplicationFields.ParentJobOpportunity] = entity.ToEntityReference();
            _newApplication[EmploymentApplicationFields.ParentJobOpportunity] = entity.GetAttributeValue<XmlEntityReference>(JobOpportunityFields.HiringFor);
        }
        private static OptionSetValue MapOptionSetValue(int key)
        {
            return new OptionSetValue(key);
        }
    }
}