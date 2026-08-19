using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.Infrastructure.Auth;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Http;
using System.Runtime.InteropServices;
using System.Net;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    internal class FlowRequestBody
    {
        public string? UserPrincipalName { get; set; }
    }
    internal class FlowResponse
    {
        [JsonPropertyName("statusCode")]
        public string? StatusCode { get; set; }
        [JsonPropertyName("body")]
        public FlowResponseBody? Body { get; set; }
    }
    internal class FlowResponseBody
    {
        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("azureObjectId")]
        public string? AzureObjectId { get; set; }
    }
    public class CoreStaffMemberFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string _correlationId;
        private bool _resolutionFailed = false;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
        private CreateHttpRequest? _httpRequest;
        private Entity? _systemUser;
        private string? _errorMessage;
        private CoreStaffMember _emptyStaffMember = new CoreStaffMember();
        // TEMPORARILY HARDCODED FOR DEV
        private string SYNC_FLOW_ENDPOINT = "https://1d7c41c84b08e1f3a61511f0059c0d.c0.environment.api.powerplatform.com:443/powerautomate/automations/direct/cu/12/workflows/2540c1018d9e41c3bd0ffb71bc75cc52/triggers/manual/paths/invoke?api-version=1&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=FC-BXGYlU_hqbETq8YqfDEqk83lvKV4aV1d8Fnq9Ra4";
        public CoreStaffMemberFactory(IOrganizationService service, ITracingService tracer, string correlationId)
        {
            _service = service;
            _tracer = tracer;
            _correlationId = correlationId;
        }
        public (CoreStaffMember staff, string? error) Build(StaffMemberSnapshot syn, string[] attributes)
        {
            _tracer.Trace("Entered CoreStaffMemberFactory.Build");
            _tracer.Trace("attributes string[]: {0}",
                JsonSerializer.Serialize(
                    attributes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            _coreStaffQuery = new CoreStaffMembers(_service, _tracer);

            _systemuserQuery = new Systemusers(_service, _tracer);

            Entity core;

            if (string.IsNullOrWhiteSpace(syn.SourceOfTruthId))
            {
                _tracer.Trace("Syn Staff Snapshot has no SourceofTruthId. Calling _coreStaffQuery.GetEntityByStaffId");
                core = _coreStaffQuery.GetEntityByStaffId(syn.StaffId);
            }
            else
            {
                _tracer.Trace("Syn Staff Snapshot HAS SourceofTruthId. Calling _coreStaffQuery.GetEntityByTruth");
                core = _coreStaffQuery.GetEntityByTruth(syn.SourceOfTruthId!);
            }

            if (syn.OccupEmail == null && syn.NetworkLogin == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no OccupEmail and/or NetworkLogin");
                throw new InvalidPluginExecutionException("Unable to resolve Syn Staff Member");
            }

            _tracer.Trace("calling _systemuserQuery.GetReferenceByMetadata with syn.OccupEmail: {0} and syn.NetworkLogin: {1}", syn.OccupEmail, syn.NetworkLogin);
            EntityReference systemUserRef = _systemuserQuery.GetReferenceByMetadata(syn.OccupEmail!, syn.NetworkLogin!);

            /* ATTEMPT TO FORCE SYN USER ACCOUNT */
            // only if ActiveFlag is true, call HTTP triggered Power Automate flow
            _tracer.Trace("Determining whether to run force sync flow with conditions systemuserref == null: {0} and syn.ActiveFlag: {1}", systemUserRef == null, syn.ActiveFlag);
            if (systemUserRef == null && syn.ActiveFlag)
            {
                _tracer.Trace("systemUserRef is null and syn.ActiveFlag is true, running force sync user branch");
                // retrieve azure id (not via systemuser)

                if (syn.OccupEmail == null)
                {
                    _tracer.Trace("Syn Staff Snapshot has no OccupEmail, unable to run ForceUserSync query");
                    throw new InvalidPluginExecutionException("Unable to resolve Core Staff Member");
                }

                _tracer.Trace("Create HttpRequest");

                _httpRequest = new CreateHttpRequest(_tracer);

                _tracer.Trace("Calling flow and retrieving response");
                var response = _httpRequest.TriggerAnonFlow(
                    SYNC_FLOW_ENDPOINT,
                    syn.OccupEmail,
                    _correlationId
                );

                if (response.status != HttpStatusCode.OK)
                {
                    _tracer.Trace("Flow response status != OK, returning empty staff member.");
                    return (_emptyStaffMember, $"{response.status} {response.body}");
                }

                _tracer.Trace("Flow response: {0}", response);

                var flowResponse = JsonSerializer.Deserialize<FlowResponse>(
                    response.body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                )!;

                var azureId = flowResponse?.Body?.AzureObjectId;

                _tracer.Trace("httpRequest flowResponse?.Body?.AzureObjectId: {0}", azureId);

                if (!string.IsNullOrWhiteSpace(azureId))
                {
                    systemUserRef = _systemuserQuery.GetReferenceByAzureId(azureId!);

                    _tracer.Trace("Retrieving systemUser metadata using systemUserRef.Id: {0}", systemUserRef.Id.ToString());

                    _systemUser = _systemuserQuery.GetMetadataById(systemUserRef.Id);
                }
                else
                {
                    _tracer.Trace("Unable to force syn user via power automate flow");
                    _resolutionFailed = true;
                    systemUserRef = null;
                    if (flowResponse?.Body?.ErrorMessage != null)
                    {
                        _tracer.Trace(
                            "RETURNING EMPTY STAFF MEMBER. Error: {0}",
                            flowResponse?.Body?.ErrorMessage
                        );
                        return (_emptyStaffMember, flowResponse?.Body?.ErrorMessage);
                    }
                }
            }

            if (systemUserRef == null || systemUserRef.Id == Guid.Empty)
            {
                _tracer.Trace("systemUserRef is null or Id is empty.");
                _resolutionFailed = true;
            }

            CoreStaffMember updateCore;
            CoreStaffMember createCore;

            if (core.Id != Guid.Empty)
            {
                _tracer.Trace("core.Id ({0}) not empty, entered updateCore and setting IsCreate to false", core.Id);
                // create new DTO with only dirty attributes
                updateCore = new CoreStaffMember()
                {
                    IsCreate = false,
                    CoreStaffMemberId = core.Id
                };

                /* update dirty attributes */
                // identity
                if (attributes.Contains("pgrowth_staffid")) MapStaffId(syn, updateCore);
                // Retrieve User
                if (
                    GetAttributeDelta.Execute<EntityReference>("pgrowth_user", core, null) == null 
                    && !_resolutionFailed
                    && systemUserRef != null
                    && systemUserRef.Id != Guid.Empty
                )
                {
                    updateCore.User = systemUserRef;
                }

                if (attributes.Contains("pgrowth_sourceoftruthid")) MapSourceOfTruthId(syn, updateCore);

                // Retrieve AzureObjectId
                if (GetAttributeDelta.Execute<string>(CoreStaffMemberFields.AzureObjectId, core, null) == null && !_resolutionFailed && _systemUser != null)
                {
                    updateCore.AzureObjectId = GetAttributeDelta.Execute<Guid>("azureactivedirectoryobjectid", _systemUser, null).ToString();
                }

                // status
                if (attributes.Contains(SynStaffMemberFields.ActiveFlag))
                    MapIsActive(syn, updateCore);

                if (
                    (attributes.Contains(SynStaffMemberFields.Status) && syn.Status != null)
                    || (attributes.Contains(SynStaffMemberFields.StatusReason) && syn.StatusReason != null))
                    MapStatus(syn, updateCore);

                // job metadata
                if (attributes.Contains(SynStaffMemberFields.StaffCampus))
                    MapCampus(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.StaffDepartment))
                    MapDepartment(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.StaffCategory))
                    MapCategory(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.StartDate) ||
                    attributes.Contains(SynStaffMemberFields.LeavingDate))
                    MapEmploymentDates(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.NetworkLogin))
                    MapNetworkLogin(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.OccupEmail) ||
                    attributes.Contains(SynStaffMemberFields.Email))
                    MapEmails(syn, updateCore);

                // personal details
                // construct display name
                if (attributes.Contains(SynStaffMemberFields.NameInternal) ||
                    attributes.Contains(SynStaffMemberFields.Given1) ||
                    attributes.Contains(SynStaffMemberFields.Given2) ||
                    attributes.Contains(SynStaffMemberFields.Surname) ||
                    attributes.Contains(SynStaffMemberFields.StaffPreferredName))
                {
                    _tracer.Trace("in updateCore MapName");
                    MapName(syn, updateCore);
                }

                if (attributes.Contains(SynStaffMemberFields.Gender))
                    MapGender(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.SurnamePrevious))
                    MapPreviousSurname(syn, updateCore);

                if (attributes.Contains(SynStaffMemberFields.MaidenName))
                    MapMaidenName(syn, updateCore);

                /// group membership
                updateCore.InUserGroup = !_resolutionFailed;

                // flags
                MapFlags(syn, updateCore, _resolutionFailed);

                // sync data
                MapLastSync(updateCore);

                return (updateCore, null);
            }
            else
            {
                _tracer.Trace("core.Id is empty, entered createCore and setting IsCreate to true");
                // create new DTO with all relevant fields
                createCore = new CoreStaffMember
                {
                    SourceOfTruthId = syn.SourceOfTruthId,
                    IsCreate = true
                };

                // identity
                MapStaffId(syn, createCore);
                if (_resolutionFailed == false && _systemUser != null)
                {
                    createCore.AzureObjectId =
                        GetAttributeDelta.Execute<Guid>(
                            "azureactivedirectoryobjectid",
                            _systemUser,
                            null
                        ).ToString();

                        if (string.IsNullOrWhiteSpace(createCore.AzureObjectId))
                        {
                            throw new Exception("Retrieved AzureId is null");
                        }

                    createCore.User = systemUserRef;
                }
                createCore.User = systemUserRef;
                MapSourceOfTruthId(syn, createCore);

                // status
                MapIsActive(syn, createCore);
                MapStatus(syn, createCore);

                // job metadata
                MapCampus(syn, createCore);
                MapDepartment(syn, createCore);
                MapCategory(syn, createCore);
                MapEmploymentDates(syn, createCore);
                MapNetworkLogin(syn, createCore);
                MapEmails(syn, createCore);

                // personal details
                MapName(syn, createCore);
                MapGender(syn, createCore);
                MapPreviousSurname(syn, createCore);
                MapMaidenName(syn, createCore);

                // group membership
                createCore.InUserGroup = !_resolutionFailed;

                // flags
                MapFlags(syn, createCore, _resolutionFailed);

                // sync
                MapLastSync(createCore);

                var json = JsonSerializer.Serialize(
                    createCore,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );

                _tracer.Trace("createCore Entity: {0}", json);

                return (createCore, null);
            }
        }
        private void MapCampus(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            _tracer.Trace("In MapCampus");
            _tracer.Trace("syn.CampusKey: {0}", syn.CampusKey);
            var campusKey = int.TryParse(syn.CampusKey, out int key) ? key : 384810005; // represets Resolution Error option
            _tracer.Trace("campusKey parsed to: {0}", key);
            target.Campus = new OptionSetValue(campusKey);
        }
        private void MapGender(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            _tracer.Trace("In MapGender");
            _tracer.Trace("syn.GenderKey: {0}", syn.GenderKey);
            var genderKey = int.TryParse(syn.GenderKey, out int key) ? key : 384810005; // represets Resolution Error option
            _tracer.Trace("genderKey parsed to: {0}", key);
            target.Gender = new OptionSetValue(genderKey);
        }
        private void MapName(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.GivenName1 = syn.Given1;
            target.GivenName2 = syn.Given2;
            target.LastName = syn.Surname;
            target.PreferredName = syn.StaffPreferredName;
            target.DisplayName = $"{syn.StaffPreferredName} {syn.Surname}";
            target.FullName = $"{syn.Given1} {syn.Surname}";
        }
        private void MapStatus(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.Status = syn.Status;
            target.StatusReason = syn.StatusReason;
        }
        private void MapEmploymentDates(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.StartDate = syn.StartDate;
            target.EndDate = syn.LeavingDate;
        }
        private void MapSourceOfTruthId(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.SourceOfTruthId = syn.SourceOfTruthId;
        }
        private void MapNetworkLogin(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.NetworkLogin = syn.NetworkLogin;
        }
        private void MapEmails(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.WorkEmail = syn.OccupEmail;
            target.PersonalEmail = syn.Email;
        }
        private void MapIsActive(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.IsActive = syn.ActiveFlag;
        }
        private void MapLastSync(CoreStaffMember target)
        {
            target.LastSync = DateTime.UtcNow;
        }
        private void MapStaffId(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.StaffId = syn.StaffId;
        }
        private void MapPreviousSurname(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.PreviousLastName = syn.SurnamePrevious;
        }
        private void MapMaidenName(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.MaidenName = syn.MaidenName;
        }
        private void MapFlags(StaffMemberSnapshot syn, CoreStaffMember target, bool resolutionFlag)
        {
            target.ProfileResolutionFlag = resolutionFlag;
            target.SPGSEmailAddressFlag = !string.IsNullOrWhiteSpace(syn.OccupEmail)
             ? syn.OccupEmail!.Contains("@stpauls.nsw.edu.au")
             : false;

            bool staffEmail = true;

            // if numeric, not staff login
            var staffNetworkLogin = !int.TryParse(syn.NetworkLogin, out int result);

            // if network login is still numeric id
            if (!staffNetworkLogin)
            {
                // check if email is updated to non-numeric username
                staffEmail =
                    !string.IsNullOrWhiteSpace(syn.NetworkLogin)
                    && !string.IsNullOrWhiteSpace(syn.OccupEmail)
                        ? !syn.OccupEmail!.Contains(syn.NetworkLogin)
                        : false;
            }
            target.StaffEmailAddressFlag = staffEmail;
        }
        private void MapDepartment(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            _tracer.Trace("Entered MapDepartment");

            var _departmentQuery = new CoreDepartments(_service, _tracer);

            if (syn.StaffDepartment == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no StaffDepartment, unable to resolve core department");
                throw new InvalidPluginExecutionException("Unable to resolve Core Staff Member");
            }

            _tracer.Trace("Calling _departmentQuery.GetReferenceByCode");

            var coreDeptRef = _departmentQuery.GetReferenceByCode(syn.StaffDepartment);

            _tracer.Trace("Executed _departmentQuery.GetReferenceByCode");

            if (coreDeptRef == null || coreDeptRef.Id == Guid.Empty)
            {
                _tracer.Trace("Department not found for code {0}. Using NOTSPEC.", syn.StaffDepartment);
                coreDeptRef = _departmentQuery.GetReferenceByCode("NOTSPEC");
            }

            _tracer.Trace("Setting target.CoreDepartment/Idx");

            target.CoreDepartment = coreDeptRef;
            target.CoreDepartmentIdx = coreDeptRef.Id.ToString();
        }
        private void MapCategory(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            _tracer.Trace("Entered MapCategory");

            var _categoryQuery = new CoreStaffCategories(_service, _tracer);

            if (syn.StaffCategory == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no StaffCategory, unable to resolve core category");
                throw new InvalidPluginExecutionException("Unable to resolve Core Staff Category");
            }

            _tracer.Trace("Executing _categoryQuery.GetReferenceByCode");

            var coreCategoryRef = _categoryQuery.GetReferenceByCode(syn.StaffCategory);

            _tracer.Trace("Completed _categoryQuery.GetReferenceByCode");

            if (coreCategoryRef == null || coreCategoryRef.Id == Guid.Empty)
            {
                _tracer.Trace("Category not found for code {0}. Using NOTSEL.", syn.StaffCategory);
                coreCategoryRef = _categoryQuery.GetReferenceByCode("NOTSEL");
            }

            _tracer.Trace("Setting target.Category/Idx");

            target.CoreStaffCategory = coreCategoryRef;
            target.CoreStaffCategoryIdx = coreCategoryRef.Id.ToString();
        }
    }
}