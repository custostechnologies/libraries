using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class SynJobPositionMapper
    {
        private IDataverseLogger _logger;
        private AttributeCollection _attributes;
        private Entity _target;
        public SynJobPositionMapper(IDataverseLogger logger, Entity target, AttributeCollection attributes)
        {
            _logger = logger;
            _target = target;
            _attributes = attributes;
        }
        public Entity ToEntity(JobPositionSnapshot snap)
        {
            _logger.WriteLog("in SynJobPositionMapper.ToEntity(), _attributes: {0}", string.Join(", ", _attributes.Keys));
            _logger.WriteLog("Hash key discrepancy detected for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
            try
            {
                if (snap.SynMirrorId != Guid.Empty)
                {
                    SetDelta(SynJobPositionFields.Id, snap.SynMirrorId);
                }

                /* SET TARGET DELTAS */
                SetDelta(SynJobPositionFields.RecordTitle, snap.Name);
                SetDelta(SynJobPositionFields.JobPositionDescription, snap.JobPositionDescription);
                SetDelta(SynJobPositionFields.StaffJobPositionsSeq, snap.StaffJobPositionsSeq);
                SetDelta(SynJobPositionFields.ReportsTo, snap.SynReportsTo);
                SetDelta(SynJobPositionFields.StaffId, snap.StaffId);
                SetDelta(SynJobPositionFields.StaffJobPositionFTE, snap.AssignmentFte);
                SetDelta(SynJobPositionFields.JobPositionFTERequired, snap.FteRequired);
                SetDelta(SynJobPositionFields.PlaintextHash, snap.PlaintextHash);
                SetDelta(SynJobPositionFields.EncodedHash, snap.EncodedHash);
                SetDelta(SynJobPositionFields.StaffJobPositionActiveFlag, snap.IsActive);
                SetDelta(SynJobPositionFields.StaffJobPositionStartDate, snap.StartDate);
                SetDelta(SynJobPositionFields.StaffJobPositionEndDate, snap.EndDate);
                SetDelta(SynJobPositionFields.SourceOfTruthId, snap.SourceOfTruthId);


                // status (calculated in snapshot factory)
                SetDelta(SynStaffMemberFields.Status, snap.Status);
                SetDelta(SynStaffMemberFields.StatusReason, snap.StatusReason);

                // sync metadata
                _target[SynStaffMemberFields.EncodedHash] = snap.EncodedHash;
                _target[SynStaffMemberFields.PlaintextHash] = snap.PlaintextHash;
                _target[SynStaffMemberFields.LastSuccessfulSync] = DateTime.UtcNow;

                _logger.WriteLog("Update or create request entity mapped from snapshot");
                _logger.WriteLog("Target attributes set  via SynJobPositionMapper.ToEntity for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
                _logger.WriteLog(
                    "Final encoded hash in target: {0}",
                    _target.GetAttributeValue<int?>("pgrowth_encodedhash")
                );

                return _target;
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error SynJobPositionMapper.ToEntity: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("InnerException SynJobPositionMapper.ToEntity: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        private void SetDelta<T>(string attributeName, T value)
        {
            _logger.WriteLog("SetDelta(attributeName: {0}, value: {1}) called.", attributeName, value);

            if (_attributes.Contains(attributeName))
            {
                SetAttributeDelta.Execute(_target, attributeName, value);
            }
        }
    }
}