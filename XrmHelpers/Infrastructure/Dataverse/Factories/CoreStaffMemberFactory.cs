<<<<<<< HEAD
using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.Infrastructure.Auth;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
=======
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Http;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    internal class FlowRequestBody
    {
<<<<<<< HEAD
        public string? UserPrincipalName { get; set; }
=======
        public string? upn { get; set; }
        public string? correlationId { get; set; }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
    }
    internal class FlowResponse
    {
        [JsonPropertyName("statusCode")]
<<<<<<< HEAD
        public string? StatusCode { get; set; }
        [JsonPropertyName("body")]
        public FlowResponseBody? Body { get; set; }
=======
        public string? statusCode { get; set; }
        [JsonPropertyName("body")]
        public FlowResponseBody? body { get; set; }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
    }
    internal class FlowResponseBody
    {
        [JsonPropertyName("errorMessage")]
<<<<<<< HEAD
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("azureObjectId")]
        public string? AzureObjectId { get; set; }
=======
        public string? errorMessage { get; set; }
        [JsonPropertyName("azureObjectId")]
        public string? azureObjectId { get; set; }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
    }
    public class CoreStaffMemberFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string _correlationId;
        private bool _resolutionFailed = false;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
<<<<<<< HEAD
        private CreateHttpRequest? _httpRequest;
        private Entity? _systemUser;
        private string? _errorMessage;
        private CoreStaffMember _emptyStaffMember = new CoreStaffMember();
        // TEMPORARILY HARDCODED FOR DEV
        private string SYNC_FLOW_ENDPOINT = "https://1d7c41c84b08e1f3a61511f0059c0d.c0.environment.api.powerplatform.com:443/powerautomate/automations/direct/cu/12/workflows/2540c1018d9e41c3bd0ffb71bc75cc52/triggers/manual/paths/invoke?api-version=1&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=FC-BXGYlU_hqbETq8YqfDEqk83lvKV4aV1d8Fnq9Ra4";
=======
        private EnvironmentVariableValues? _envVarQuery;
        private CreateHttpRequest? _httpRequest;
        private Entity? _systemUser;
        private bool _create = false;
        private string ENDPOINT_ENV_VAR_SCHEMA_NAME = "pgrowth_XrmHelpers_FlowEndpoint_ForceUserSync";
        private string SYNC_FLOW_ENDPOINT = "";
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        public CoreStaffMemberFactory(IOrganizationService service, ITracingService tracer, string correlationId)
        {
            _service = service;
            _tracer = tracer;
            _correlationId = correlationId;
        }
<<<<<<< HEAD
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

            if (syn.SourceOfTruthId == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no Source of Truth Id");
=======
        public (CoreStaffMember staff, bool createAsInactive, string? error) Build(StaffMemberSnapshot syn, string[] attributes)
        {
            _tracer.Trace("Entered CoreStaffMemberFactory.Build");
            _coreStaffQuery = new CoreStaffMembers(_service, _tracer);
            _systemuserQuery = new Systemusers(_service, _tracer);
            _envVarQuery = new EnvironmentVariableValues(_tracer, _service);

            Entity core;

            if (string.IsNullOrWhiteSpace(syn.SourceOfTruthId))
            {
                _tracer.Trace("Syn Staff Snapshot has no SourceofTruthId. Calling _coreStaffQuery.GetEntityByStaffId");
>>>>>>> source-temp/refactor/decouple-xrmhelpers
                core = _coreStaffQuery.GetEntityByStaffId(syn.StaffId);
            }
            else
            {
<<<<<<< HEAD
                core = _coreStaffQuery.GetEntityByTruth(syn.SourceOfTruthId);
            }

            if (syn.OccupEmail == null && syn.NetworkLogin == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no OccupEmail and/or NetworkLogin");
                throw new InvalidPluginExecutionException("Unable to resolve Syn Staff Member");
            }
=======
                _tracer.Trace("Syn Staff Snapshot HAS SourceofTruthId. Calling _coreStaffQuery.GetEntityByTruth");
                core = _coreStaffQuery.GetEntityByTruth(syn.SourceOfTruthId!);

                if (core == null)
                {
                    _tracer.Trace("No Core Staff Member found via GetEntityByTruth. Trying again with StaffId");
                    core = _coreStaffQuery.GetEntityByStaffId(syn.StaffId);
                }
            }

            _create = core == null;

            /**/
            _tracer.Trace("Core Staff Member queries complete. _create set to {0}. Querying systemusers.", _create);

            if (syn.OccupEmail == null && syn.NetworkLogin == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no OccupEmail and/or NetworkLogin. Exiting early.");
                throw new InvalidPluginExecutionException("Unable to resolve Syn Staff Member");
            }

            _tracer.Trace("calling _systemuserQuery.GetReferenceByMetadata with syn.OccupEmail: {0} and syn.NetworkLogin: {1}", syn.OccupEmail, syn.NetworkLogin);

>>>>>>> source-temp/refactor/decouple-xrmhelpers
            EntityReference systemUserRef = _systemuserQuery.GetReferenceByMetadata(syn.OccupEmail!, syn.NetworkLogin!);

            /* ATTEMPT TO FORCE SYN USER ACCOUNT */
            // only if ActiveFlag is true, call HTTP triggered Power Automate flow
<<<<<<< HEAD
            if (
                (systemUserRef == null || systemUserRef.Id == Guid.Empty)
                && syn.ActiveFlag
            )
            {
                _tracer.Trace("systemUserRef.Id is null or empty");
                // retrieve azure id (not via systemuser)

=======
            _tracer.Trace("Determining whether to run force sync flow with conditions systemuserref == null: {0} and syn.ActiveFlag: {1}", systemUserRef == null, syn.ActiveFlag);

            if (systemUserRef == null && syn.ActiveFlag)
            {
                _tracer.Trace("systemUserRef is null and syn.ActiveFlag is true, running force sync user branch");
                // retrieve azure id (not via systemuser)

                _tracer.Trace("Retrieving flow endpoint for current environment");

                SYNC_FLOW_ENDPOINT = _envVarQuery.GetValue(ENDPOINT_ENV_VAR_SCHEMA_NAME).Value!;

>>>>>>> source-temp/refactor/decouple-xrmhelpers
                if (syn.OccupEmail == null)
                {
                    _tracer.Trace("Syn Staff Snapshot has no OccupEmail, unable to run ForceUserSync query");
                    throw new InvalidPluginExecutionException("Unable to resolve Core Staff Member");
                }

<<<<<<< HEAD
                _httpRequest = new CreateHttpRequest(_tracer);

                string response = _httpRequest.TriggerAnonFlow(
=======
                _tracer.Trace("Create HttpRequest");

                _httpRequest = new CreateHttpRequest(_tracer);

                _tracer.Trace("Calling flow and retrieving response");
                var response = _httpRequest.TriggerAnonFlow(
>>>>>>> source-temp/refactor/decouple-xrmhelpers
                    SYNC_FLOW_ENDPOINT,
                    syn.OccupEmail,
                    _correlationId
                );

<<<<<<< HEAD
                _tracer.Trace("Flow response: {0}", response);

                FlowResponseBody flowResponseBody = JsonSerializer.Deserialize<FlowResponseBody>(
                    response,
=======
                if (response.status != HttpStatusCode.OK)
                {
                    _tracer.Trace("Flow response status != OK, unable to resolve systemuser.");
                }

                _tracer.Trace("Flow response in CoreStaffMemberFactory before deserialisation: {0}", response);

                var flowResponseBody = JsonSerializer.Deserialize<FlowResponseBody>(
                    response.body,
>>>>>>> source-temp/refactor/decouple-xrmhelpers
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                )!;

<<<<<<< HEAD
                var azureId = flowResponseBody?.AzureObjectId;

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
                    systemUserRef = new EntityReference("systemuser");
                    if (flowResponseBody?.ErrorMessage != null)
                    {
                        return (_emptyStaffMember, flowResponseBody?.ErrorMessage);
                    } else
                    {
                        return (_emptyStaffMember, "Unable to force sync user via Power Automate flow.");
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
                if (GetAttributeDelta.Execute<EntityReference>("pgrowth_user", core, null) == null && !_resolutionFailed)
                {
                    updateCore.User = systemUserRef;
                }

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
                // create new DTO with all relevant fields
                createCore = new CoreStaffMember
                {
                    SourceOfTruthId = syn.SourceOfTruthId,
                    IsCreate = true
                };

                // identity
                MapStaffId(syn, createCore);
                if (!_resolutionFailed && _systemUser != null)
                {
                    createCore.AzureObjectId =
                        GetAttributeDelta.Execute<Guid>(
                            "azureactivedirectoryobjectid",
                            _systemUser,
                            null
                        ).ToString();

                    createCore.User = systemUserRef;
                }
                createCore.User = systemUserRef;

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
=======
                var azureId = flowResponseBody?.azureObjectId;
                _tracer.Trace("httpRequest flowResponseBody?.AzureObjectId: {0}", azureId);

                if (!string.IsNullOrWhiteSpace(azureId))
                {
                    _tracer.Trace("azureId returned from flow is not null. Retrieving EntityReference via _systemUserQuery.GetReferenceByAzureId");
                    systemUserRef = _systemuserQuery.GetReferenceByAzureId(azureId!);

                    if (systemUserRef != null)
                    {
                        _tracer.Trace("systemUserRef is not null. Retrieving systemUser metadata using systemUserRef.Id: {0}", systemUserRef.Id.ToString());
                        _systemUser = _systemuserQuery.GetMetadataById(systemUserRef.Id);
                    }
                }
                else
                {
                    _tracer.Trace("Unable to force syn user via power automate flow (azureId returned null). Setting _resolutionFailed to true.");
                    _resolutionFailed = true;
                }
            }
            else if (systemUserRef != null)
            {
                _tracer.Trace("ELSE IF BRANCH UNDER CALL POWER AUTOMATE FLOW BLOCK, systemUserRef is not null. Retrieving systemUser metadata using systemUserRef.Id: {0}", systemUserRef.Id.ToString());
                _systemUser = _systemuserQuery.GetMetadataById(systemUserRef.Id);
            }

            CoreStaffMember coreStaffMember;
            if (core != null) _tracer.Trace("core not null and _create = true, entered coreStaffMember and setting IsCreate to false", core.Id);
            // create new DTO with only dirty attributes
            coreStaffMember = core != null
                ? new CoreStaffMember()
                {
                    IsCreate = _create,
                    CoreStaffMemberId = core.Id
                }
                : new CoreStaffMember()
                {
                    IsCreate = _create
                };

            /* update dirty attributes */
            // identity
            MapStaffId(syn, coreStaffMember);
            // Retrieve User
            if (systemUserRef != null && systemUserRef.Id != Guid.Empty && _systemUser != null)
            {
                coreStaffMember.User = systemUserRef;
                coreStaffMember.AzureObjectId = GetAttributeDelta.Execute<Guid>(
                        "azureactivedirectoryobjectid",
                        _systemUser,
                        null
                    ).ToString();

                if (string.IsNullOrWhiteSpace(coreStaffMember.AzureObjectId))
                {
                    throw new Exception("Retrieved AzureId is null");
                }
            }

            MapSourceOfTruthId(syn, coreStaffMember);

            // status
            MapIsActive(syn, coreStaffMember, attributes);
            MapStatus(syn, coreStaffMember);

            // personal details
            MapName(syn, coreStaffMember, attributes);
            MapGender(syn, coreStaffMember, attributes);
            MapMaidenName(syn, coreStaffMember, attributes);
            MapPreviousSurname(syn, coreStaffMember, attributes);

            // job metadata
            MapCampus(syn, coreStaffMember, attributes);
            MapDepartment(syn, coreStaffMember, attributes);
            MapCategory(syn, coreStaffMember, attributes);
            MapEmploymentDates(syn, coreStaffMember, attributes);
            MapNetworkLogin(syn, coreStaffMember, attributes);
            MapEmails(syn, coreStaffMember, attributes);

            // flags
            MapFlags(syn, coreStaffMember, _resolutionFailed, attributes);
            coreStaffMember.InUserGroup = !_resolutionFailed;

            // sync data
            MapLastSync(coreStaffMember);
            if (_create && syn.ActiveFlag == false)
            {
                _tracer.Trace("CREATE CoreStaffMember with a status of INACTIVE. Returning with createAsInactive = TRUE to perform subsequent UPDATE operation to inactive the record afyter CREATE.");
                return (coreStaffMember, true, null);
            }
            return (coreStaffMember, false, null);
        }
        private void MapCampus(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapCampus");
            if (_create || attributes.Contains(SynStaffMemberFields.StaffCampus))
            {
                _tracer.Trace("syn.CampusKey: {0}", syn.CampusKey);
                var campusKey = int.TryParse(syn.CampusKey, out int key) ? key : 384810005; // represets Resolution Error option
                _tracer.Trace("campusKey parsed to: {0}", key);
                target.Campus = new OptionSetValue(campusKey);
            }
        }
        private void MapGender(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapGender");
            if (_create || attributes.Contains(SynStaffMemberFields.Gender))
            {
                _tracer.Trace("syn.GenderKey: {0}", syn.GenderKey);
                var genderKey = int.TryParse(syn.GenderKey, out int key) ? key : 384810005; // represets Resolution Error option
                _tracer.Trace("genderKey parsed to: {0}", key);
                target.Gender = new OptionSetValue(genderKey);
            }
        }
        private void MapName(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapName");
            bool contains = attributes.Contains(SynStaffMemberFields.NameInternal)
                || attributes.Contains(SynStaffMemberFields.Given1)
                || attributes.Contains(SynStaffMemberFields.Given2)
                || attributes.Contains(SynStaffMemberFields.Surname)
                || attributes.Contains(SynStaffMemberFields.StaffPreferredName);

            if (_create || contains)
            {
                target.GivenName1 = syn.Given1;
                target.GivenName2 = syn.Given2;
                target.LastName = syn.Surname;
                target.PreferredName = syn.StaffPreferredName;
                target.DisplayName = $"{syn.StaffPreferredName} {syn.Surname}";
                target.FullName = $"{syn.Given1} {syn.Surname}";
                _tracer.Trace("MapName set GivenName1: {0}, GivenName2: {1}, LastName: {2}, PreferredName: {3}, DisplayName: {4}, FullName: {5}",
                    target.GivenName1,
                    target.GivenName2,
                    target.LastName,
                    target.PreferredName,
                    target.DisplayName,
                    target.FullName
                );
            }

        }
        private void MapStatus(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            _tracer.Trace("In MapStatus with syn.Status: {0}, syn.StatusReason: {1}", syn.Status, syn.StatusReason);
            target.Status = syn.Status;
            target.StatusReason = syn.StatusReason;
        }
        private void MapEmploymentDates(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapEmploymentDates");
            if ((_create || attributes.Contains(SynStaffMemberFields.StartDate)) && syn.StartDate.HasValue)
            {
                target.StartDate = syn.StartDate;
                _tracer.Trace("Set target.StartDate to: {0}", target.StartDate.Value);
            }
            ;
            if ((_create || attributes.Contains(SynStaffMemberFields.LeavingDate)) && syn.LeavingDate.HasValue)
            {
                target.EndDate = syn.LeavingDate;
                _tracer.Trace("Set target.EndDate to: {0}", target.EndDate.Value);
            }
            ;
        }
        private void MapSourceOfTruthId(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.SourceOfTruthId = syn.SourceOfTruthId;
        }
        private void MapNetworkLogin(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapNetworkLogin.");
            if (_create || attributes.Contains(SynStaffMemberFields.NetworkLogin))
            {
                target.NetworkLogin = syn.NetworkLogin;
                _tracer.Trace("Set target.NetworkLogin to: {0}", target.NetworkLogin);
            }
        }
        private void MapEmails(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapEmails.");
            if (_create || attributes.Contains(SynStaffMemberFields.OccupEmail))
            {
                target.WorkEmail = syn.OccupEmail;
                _tracer.Trace("Set target.WorkEmail to: {0}", target.WorkEmail);
            }
            if (_create || attributes.Contains(SynStaffMemberFields.Email))
            {
                target.PersonalEmail = syn.Email;
                _tracer.Trace("Set target.PersonalEmail to: {0}", target.PersonalEmail);
            }
        }
        private void MapIsActive(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapIsActive.");
            if (_create || attributes.Contains(SynStaffMemberFields.ActiveFlag))
            {
                target.IsActive = syn.ActiveFlag;
                _tracer.Trace("Set target.IsActive to: {0}", target.IsActive);
            }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        }
        private void MapLastSync(CoreStaffMember target)
        {
            target.LastSync = DateTime.UtcNow;
        }
        private void MapStaffId(StaffMemberSnapshot syn, CoreStaffMember target)
        {
            target.StaffId = syn.StaffId;
        }
<<<<<<< HEAD
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
=======
        private void MapPreviousSurname(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapPreviousSurname.");
            if (_create || attributes.Contains(SynStaffMemberFields.SurnamePrevious))
            {
                target.PreviousLastName = syn.SurnamePrevious;
                _tracer.Trace("Set target.PreviousLastName to: {0}", target.PreviousLastName);
            }
        }
        private void MapMaidenName(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapMaidenName.");
            if (_create || attributes.Contains(SynStaffMemberFields.MaidenName))
            {
                target.MaidenName = syn.MaidenName;
                _tracer.Trace("Set target.MaidenName to: {0}", target.MaidenName);
            }
        }
        private void MapFlags(StaffMemberSnapshot syn, CoreStaffMember target, bool resolutionFlag, string[] attributes)
        {
            _tracer.Trace("In MapFlags");
            bool contains = attributes.Contains(SynStaffMemberFields.OccupEmail)
                && attributes.Contains(SynStaffMemberFields.Email)
                && attributes.Contains(SynStaffMemberFields.NetworkLogin);

            if (_create || contains)
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
                target.InUserGroup = !_resolutionFailed;
            }
        }
        private void MapDepartment(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapDepartment.");
            if (_create || attributes.Contains(SynStaffMemberFields.StaffDepartment))
            {
                var _departmentQuery = new CoreDepartments(_service, _tracer);

                EntityReference coreDeptRef;

                if (syn.StaffDepartment == null)
                {
                    _tracer.Trace("Syn Staff Snapshot has no StaffDepartment, setting to NOTSPEC department.");
                    coreDeptRef = null;
                }
                else
                {
                    _tracer.Trace("Syn Staff Snapshot has StaffDepartment with code: {0}.", syn.StaffDepartment);
                    _tracer.Trace("Calling _departmentQuery.GetReferenceByCode");
                    coreDeptRef = _departmentQuery.GetReferenceByCode(syn.StaffDepartment);
                }

                if (coreDeptRef == null || coreDeptRef.Id == Guid.Empty)
                {
                    _tracer.Trace("Department not found for code {0}. Using NOTSPEC.", syn.StaffDepartment);
                    coreDeptRef = _departmentQuery.GetReferenceByCode("NOTSPEC");
                }

                _tracer.Trace("Setting target.CoreDepartment/Idx");
                target.CoreDepartment = coreDeptRef;
                target.CoreDepartmentIdx = coreDeptRef.Id.ToString();
            }
        }
        private void MapCategory(StaffMemberSnapshot syn, CoreStaffMember target, string[] attributes)
        {
            _tracer.Trace("In MapCategory.");
            if (_create || attributes.Contains(SynStaffMemberFields.StaffCategory))
            {
                var _categoryQuery = new CoreStaffCategories(_service, _tracer);

                EntityReference coreCategoryRef;

                if (syn.StaffCategory == null)
                {
                    _tracer.Trace("Syn Staff Snapshot has no StaffCategory, unable to resolve core category");
                    coreCategoryRef = null;
                }
                else
                {
                    _tracer.Trace("Executing _categoryQuery.GetReferenceByCode");
                    coreCategoryRef = _categoryQuery.GetReferenceByCode(syn.StaffCategory);
                    _tracer.Trace("Completed _categoryQuery.GetReferenceByCode");
                }

                if (coreCategoryRef == null || coreCategoryRef.Id == Guid.Empty)
                {
                    _tracer.Trace("Category not found for code {0}. Using NOTSEL.", syn.StaffCategory);
                    coreCategoryRef = _categoryQuery.GetReferenceByCode("NOTSEL");
                }

                _tracer.Trace("Setting target.Category/Idx");

                target.CoreStaffCategory = coreCategoryRef;
                target.CoreStaffCategoryIdx = coreCategoryRef.Id.ToString();
            }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        }
    }
}