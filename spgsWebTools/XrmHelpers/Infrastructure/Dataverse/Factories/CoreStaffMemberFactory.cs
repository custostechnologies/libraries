using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class CoreStaffMemberFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private bool _resolutionFailed = false;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
        public CoreStaffMemberFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public CoreStaffMember Build(StaffMemberSnapshot syn, string[] attributes)
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
                core = _coreStaffQuery.GetEntityByStaffId(syn.StaffId);
            }
            else
            {
                core = _coreStaffQuery.GetEntityByTruth(syn.SourceOfTruthId);
            }

            if (syn.OccupEmail == null && syn.NetworkLogin == null)
            {
                _tracer.Trace("Syn Staff Snapshot has no OccupEmail and/or NetworkLogin");
                throw new InvalidPluginExecutionException("Unable to resolve Syn Staff Member");
            }
            EntityReference systemUserRef = _systemuserQuery.GetReferenceByMetadata(syn.OccupEmail!, syn.NetworkLogin!);

            if (systemUserRef == null || systemUserRef.Id == Guid.Empty)
            {
                _tracer.Trace("systemUserRef.Id is null or empty");
                // retrieve azure id (not via systemuser)
                ForceUserSync _forceSyncService = new ForceUserSync(_service, _tracer);

                if (syn.OccupEmail == null)
                {
                    _tracer.Trace("Syn Staff Snapshot has no OccupEmail, unable to run ForceUserSync query");
                    throw new InvalidPluginExecutionException("Unable to resolve Core Staff Member");
                }
                var newUserId = _forceSyncService.Execute(syn.OccupEmail);

                if (newUserId == null)
                {
                    _tracer.Trace("Unable to force syn user");
                    _resolutionFailed = true;
                }
                else
                {
                    systemUserRef = new EntityReference(
                        "systemuser",
                        (Guid)newUserId
                    );
                }
            }

            if (systemUserRef == null || systemUserRef.Id == Guid.Empty)
            {
                _tracer.Trace("systemUserRef is null or Id is empty.");
                throw new InvalidPluginExecutionException("Invalid systemUserRef.");
            }

            _tracer.Trace("Retrieving systemUser metadata using systemUserRef.Id: {0}", systemUserRef.Id.ToString());

            Entity systemUser = _systemuserQuery.GetMetadataById(systemUserRef.Id);

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
                if (GetAttributeDelta.Execute<EntityReference>("pgrowth_user", core) == null && !_resolutionFailed)
                {
                    updateCore.User = systemUserRef;
                }

                // Retrieve AzureObjectId
                if (GetAttributeDelta.Execute<string>(CoreStaffMemberFields.AzureObjectId, core) == null && !_resolutionFailed)
                {
                    updateCore.AzureObjectId = GetAttributeDelta.Execute<Guid>("azureactivedirectoryobjectid", systemUser).ToString();
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

                return updateCore;
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
                if (!_resolutionFailed)
                {
                    createCore.AzureObjectId =
                        GetAttributeDelta.Execute<Guid>(
                            "azureactivedirectoryobjectid",
                            systemUser
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

                return createCore;
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