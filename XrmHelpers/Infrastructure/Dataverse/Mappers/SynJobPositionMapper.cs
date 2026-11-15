using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Contracts;
namespace custos.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class SynJobPositionMapper
    {
        private readonly IDataverseLogger _logger;
        public SynJobPositionMapper(IDataverseLoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create<SynJobPositionMapper>();
        }
        public Entity ToEntity(Entity target, AttributeCollection attributes, JobPositionSnapshot snap)
        {
            _logger.WriteLog("in SynJobPositionMapper.ToEntity(), _attributes: {0}", string.Join(", ", attributes.Keys));
            _logger.WriteLog("Hash key discrepancy detected for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
            try
            {
                if (snap.SynMirrorId != Guid.Empty)
                {
                    SetDelta(target, attributes, SynJobPositionFields.Id, snap.SynMirrorId);
                }

                /* SET TARGET DELTAS */
                SetDelta(target, attributes, SynJobPositionFields.RecordTitle, snap.Name);
                SetDelta(target, attributes, SynJobPositionFields.JobPositionDescription, snap.JobPositionDescription);
                SetDelta(target, attributes, SynJobPositionFields.StaffJobPositionsSeq, snap.StaffJobPositionsSeq);
                SetDelta(target, attributes, SynJobPositionFields.ReportsTo, snap.SynReportsTo);
                SetDelta(target, attributes, SynJobPositionFields.StaffId, snap.StaffId);
                SetDelta(target, attributes, SynJobPositionFields.StaffJobPositionFTE, snap.AssignmentFte);
                SetDelta(target, attributes, SynJobPositionFields.JobPositionFTERequired, snap.FteRequired);
                SetDelta(target, attributes, SynJobPositionFields.PlaintextHash, snap.PlaintextHash);
                SetDelta(target, attributes, SynJobPositionFields.EncodedHash, snap.EncodedHash);
                SetDelta(target, attributes, SynJobPositionFields.StaffJobPositionActiveFlag, snap.IsActive);
                SetDelta(target, attributes, SynJobPositionFields.StaffJobPositionStartDate, snap.StartDate);
                SetDelta(target, attributes, SynJobPositionFields.StaffJobPositionEndDate, snap.EndDate);
                SetDelta(target, attributes, SynJobPositionFields.SourceOfTruthId, snap.SourceOfTruthId);


                // status (calculated in snapshot factory)
                SetDelta(target, attributes, SynStaffMemberFields.Status, snap.Status);
                SetDelta(target, attributes, SynStaffMemberFields.StatusReason, snap.StatusReason);

                // sync metadata
                target[SynStaffMemberFields.EncodedHash] = snap.EncodedHash;
                target[SynStaffMemberFields.PlaintextHash] = snap.PlaintextHash;
                target[SynStaffMemberFields.LastSuccessfulSync] = DateTime.UtcNow;

                _logger.WriteLog("Update or create request entity mapped from snapshot");
                _logger.WriteLog("Target attributes set  via SynJobPositionMapper.ToEntity for row with Staff ID: {0}, New Hash: {1}", snap.StaffId, snap.EncodedHash);
                _logger.WriteLog(
                    "Final encoded hash in target: {0}",
                    target.GetAttributeValue<int?>("pgrowth_encodedhash")
                );

                return target;
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
        private void SetDelta<T>(Entity target, AttributeCollection attributes, string attributeName, T value)
        {
            _logger.WriteLog("SetDelta(attributeName: {0}, value: {1}) called.", attributeName, value);

            if (attributes.Contains(attributeName))
            {
                SetAttributeDelta.Execute(target, attributeName, value);
            }
        }
    }
}