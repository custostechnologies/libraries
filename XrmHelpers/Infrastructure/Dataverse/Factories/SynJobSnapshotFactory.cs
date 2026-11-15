using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Constants;
using Enums = Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class SynJobSnapshotFactory
    {
        private readonly IDataverseLogger _logger;
        private bool? _forceIncoming = false;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaffjobpositions";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public SynJobSnapshotFactory(IDataverseLogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="preImage"></param>
        /// <param name="sourceRecordId"></param>
        /// <returns></returns>
        public JobPositionSnapshot Build(Entity target, Entity? preImage, Guid sourceRecordId)
        {
            _logger.WriteLog("Creating job snapshot in JobPositionSnapshot.Build");

            if (target.Contains(SynStaffMemberFields.MutePlugin))
            {
                _forceIncoming = target.GetAttributeValue<bool>(SynStaffMemberFields.MutePlugin);
                _logger.WriteLog("MutePlugin is TRUE, setting preImage to NULL to force update using incoming values.");
                preImage = null;
            }

            var sName = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage);
            var sDescription = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage);
            var sASeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage);
            var sJSeq = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage);
            var sJCode = GetAttributeDelta.Execute<string>(SynJobPositionFields.StaffJobPositionCode, target, preImage);
            var sID = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage);
            var sStaffName = GetAttributeDelta.Execute<string>(SynJobPositionFields.StaffNameInternal, target, preImage);

            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = sourceRecordId,

                Name = $"[{sID}] {sStaffName} {sDescription} (SEQ: {sASeq})",
                JobPositionDescription = sDescription,
                SynReportsTo = GetAttributeDelta.Execute<string>(SynJobPositionFields.ReportsTo, target, preImage),
                SourceOfTruthId = GetAttributeDelta.Execute<string>(SynJobPositionFields.SourceOfTruthId, target, preImage),

                // retrieve integer values
                StaffId = sID,
                JobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.JobPositionsSeq, target, preImage),
                StaffJobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage),
                StaffJobPositionCode = sJCode,
                StaffNameInternal = sStaffName,

                // retrieve float values
                AssignmentFte = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.StaffJobPositionFTE, target, preImage),
                FteRequired = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.JobPositionFTERequired, target, preImage),

                // retrieve boolean values
                IsActive = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionActiveFlag, target, preImage),

                // retrieve date times values accounting for null values
                EndDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionEndDate, target, preImage),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionStartDate, target, preImage)
            };

            _logger.WriteLog("{0} SynMirrorId: {1}", ENTITY_LOGICAL_NAME, snap.SynMirrorId.ToString());

            if (string.IsNullOrWhiteSpace(snap.SourceOfTruthId))
            {
                snap.SourceOfTruthId = Guid.NewGuid().ToString();
                _logger.WriteLog("Set new SrcOfTruthId: {0}", snap.SourceOfTruthId);
            }

            _logger.WriteLog("Calculating job assignment status and status reason");
            _logger.WriteLog(
                "SNAP EndDate.HasValue={0}, EndDate={1}, IsActive={2}",
                snap.EndDate.HasValue,
                snap.EndDate.HasValue ? snap.EndDate.Value.ToString("o") : "<null>",
                snap.IsActive
            );

            // 0 represents inactive
            if (snap.IsActive == 0 || (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow))
            {
                _logger.WriteLog("Setting status to Inactive");
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                _logger.WriteLog("Setting status to Active");
                snap.Status = new OptionSetValue((int)Enums.Status.Active);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

            _logger.WriteLog("Calulated status: {0} and statusReason: {1}",
                ((Enums.Status)snap.Status.Value).ToString(),
                ((Enums.StatusReason)snap.StatusReason.Value).ToString()
            );
            _logger.WriteLog("SNAP: {0}",
                JsonSerializer.Serialize(
                    snap,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            return snap;
        }
    }
}