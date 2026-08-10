using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class SynStaffMemberMapper
    {
        private ITracingService _tracer;
        private AttributeCollection _attributes;
        private Entity _target;
        public SynStaffMemberMapper(ITracingService tracer, Entity target, AttributeCollection attributes)
        {
            _tracer = tracer;
            _target = target;
            _attributes = attributes;
        }
        public Entity ToEntity(StaffMemberSnapshot snap)
        {
            _tracer.Trace("in SynStaffMemberMapper.ToEntity(), _attributes: {0}", string.Join(", ", _attributes.Keys));
            _tracer.Trace("Hash key discrepancy detected via ChangeDetection plugin for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
            try
            {
                if (snap.SynMirrorId != Guid.Empty)
                {
                    SetDelta(SynStaffMemberFields.SynMirrorId, snap.SynMirrorId);
                }

                /* SET TARGET DELTAS */
                // identity
                SetDelta(SynStaffMemberFields.CommunityGuid, snap.CommunityGuid);
                SetDelta(SynStaffMemberFields.StaffId, snap.StaffId);
                SetDelta(SynStaffMemberFields.SourceOfTruthId, snap.SourceOfTruthId);

                // personal details
                SetDelta(SynStaffMemberFields.Given1, snap.Given1);
                SetDelta(SynStaffMemberFields.Given2, snap.Given2);
                SetDelta(SynStaffMemberFields.NameInternal, snap.NameInternal);
                SetDelta(SynStaffMemberFields.Surname, snap.Surname);
                SetDelta(SynStaffMemberFields.MaidenName, snap.MaidenName);
                SetDelta(SynStaffMemberFields.StaffPreferredName, snap.StaffPreferredName);
                SetDelta(SynStaffMemberFields.SurnamePrevious, snap.SurnamePrevious);
                SetDeltaKey(SynStaffMemberFields.Gender, SynStaffMemberFields.GenderKey, snap.Gender, snap.GenderKey);

                // contact details
                SetDelta(SynStaffMemberFields.Email, snap.Email);

                // account metadata
                SetDelta(SynStaffMemberFields.NetworkLogin, snap.NetworkLogin);

                // job metadata
                SetDelta(SynStaffMemberFields.SchoolStaffCode, snap.SchoolStaffCode);
                SetDelta(SynStaffMemberFields.SubSchool, snap.SubSchool);
                SetDelta(SynStaffMemberFields.OccupEmail, snap.OccupEmail);
                SetDelta(SynStaffMemberFields.OccupPhone, snap.OccupPhone);
                SetDeltaKey(SynStaffMemberFields.StaffCampus, SynStaffMemberFields.CampusKey, snap.StaffCampus, snap.CampusKey);
                SetDelta(SynStaffMemberFields.StaffCategory, snap.StaffCategory);
                SetDelta(SynStaffMemberFields.StaffDepartment, snap.StaffDepartment);
                SetDelta(SynStaffMemberFields.StartDate, snap.StartDate);
                SetDelta(SynStaffMemberFields.LeavingDate, snap.LeavingDate);

                // status (calculated in snapshot factory)
                SetDelta(SynStaffMemberFields.Status, snap.Status);
                SetDelta(SynStaffMemberFields.StatusReason, snap.StatusReason);
                SetDelta(SynStaffMemberFields.ActiveFlag, snap.ActiveFlag);

                // sync metadata
                _target[SynStaffMemberFields.EncodedHash] = snap.EncodedHash;
                _target[SynStaffMemberFields.PlaintextHash] = snap.PlaintextHash;
                _target[SynStaffMemberFields.LastSuccessfulSync] = DateTime.UtcNow;

                _tracer.Trace("Update or create request entity mapped from snapshot");
                _tracer.Trace("Target attributes set  via StaffChangeDetection plugin for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
                _tracer.Trace(
                    "Final encoded hash in target: {0}",
                    _target.GetAttributeValue<int?>("pgrowth_encodedhash")
                );

                foreach (var a in _attributes)
                {
                    _tracer.Trace(
                        "{0} = {1}",
                        a.Key,
                        a.Value?.ToString() ?? "<null>"
                    );
                }

                return _target;
            }
            catch (Exception ex)
            {
                _tracer.Trace("Error SynStaffMemberMapper.ToEntity: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("InnerException SynStaffMemberMapper.ToEntity: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        private void SetDelta<T>(string attributeName, T value)
        {
            _tracer.Trace("SetDelta(attributeName: {0}, value: {1}) called.", attributeName, value);

            if (_attributes.Contains(attributeName))
            {
                SetAttributeDelta.Execute(_target, attributeName, value);
            }
        }
        private void SetDeltaKey<T>(string attributeName, string keyAttributeName, T? value, string? key)
        {
            _tracer.Trace(
                "SetDeltaKey(attributeName: {0}, keyAttributeName: {1}, value: {2}, key: {3}) called.",
                attributeName,
                keyAttributeName,
                value,
                key
            );

            if (_attributes.Contains(attributeName))
            {
                SetAttributeDelta.Execute(_target, attributeName, value);
                SetAttributeDelta.Execute(_target, keyAttributeName, key);
            }
        }
    }
}