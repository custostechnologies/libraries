using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class SynJobSnapshotFactory
    {
        private readonly ITracingService _tracer;
<<<<<<< HEAD
=======
        private bool? _forceIncoming = false;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaffjobpositions";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public SynJobSnapshotFactory(ITracingService tracer)
        {
            _tracer = tracer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="preImage"></param>
        /// <param name="sourceRecordId"></param>
        /// <returns></returns>
<<<<<<< HEAD
        public JobPositionSnapshot Build(Entity target, Entity preImage, Guid sourceRecordId)
        {
            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

=======
        public JobPositionSnapshot Build(Entity target, Entity? preImage, Guid sourceRecordId)
        {
            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

            if (target.Contains(SynStaffMemberFields.MutePlugin))
            {
                _forceIncoming = target.GetAttributeValue<bool>(SynStaffMemberFields.MutePlugin);
                _tracer.Trace("MutePlugin is TRUE, setting preImage to NULL to force update using incoming values.");
                preImage = null;
            }

            var sName = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage);
            var sDescription = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage);
            var sASeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage);
            var sJSeq = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage);
            var sJCode = GetAttributeDelta.Execute<string>(SynJobPositionFields.StaffJobPositionCode, target, preImage);
            var sID = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage);
            var sStaffName = GetAttributeDelta.Execute<string>(SynJobPositionFields.StaffNameInternal, target, preImage);

>>>>>>> source-temp/refactor/decouple-xrmhelpers
            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = sourceRecordId,

<<<<<<< HEAD
                Name = $"{GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage)} {GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage)} (SEQ: {GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage)})",
                JobPositionDescription = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage),
                JobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.JobPositionsSeq, target, preImage),
                StaffJobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage),
=======
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
>>>>>>> source-temp/refactor/decouple-xrmhelpers

                // retrieve float values
                AssignmentFte = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.StaffJobPositionFTE, target, preImage),
                FteRequired = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.JobPositionFTERequired, target, preImage),

                // retrieve boolean values
                IsActive = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionActiveFlag, target, preImage),

                // retrieve date times values accounting for null values
                EndDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionEndDate, target, preImage),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionStartDate, target, preImage)
            };

            _tracer.Trace("{0} SynMirrorId: {1}", ENTITY_LOGICAL_NAME, snap.SynMirrorId.ToString());

<<<<<<< HEAD
=======
            if (string.IsNullOrWhiteSpace(snap.SourceOfTruthId))
            {
                snap.SourceOfTruthId = Guid.NewGuid().ToString();
                _tracer.Trace("Set new SrcOfTruthId: {0}", snap.SourceOfTruthId);
            }

>>>>>>> source-temp/refactor/decouple-xrmhelpers
            _tracer.Trace("Calculating job assignment status and status reason");
            _tracer.Trace(
                "SNAP EndDate.HasValue={0}, EndDate={1}, IsActive={2}",
                snap.EndDate.HasValue,
                snap.EndDate.HasValue ? snap.EndDate.Value.ToString("o") : "<null>",
                snap.IsActive
            );
<<<<<<< HEAD
            if (
                // 0 represents inactive
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 1
            )
=======

            // 0 represents inactive
            if (snap.IsActive == 0 || (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow))
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            {
                _tracer.Trace("Setting status to Inactive");
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                _tracer.Trace("Setting status to Active");
                snap.Status = new OptionSetValue((int)Enums.Status.Active);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

<<<<<<< HEAD
            _tracer.Trace("Calulated status: {0} and statusReason: {1}", snap.Status?.Value, snap.StatusReason?.Value);

            return snap;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="preImage"></param>
        /// <param name="sourceRecordId"></param>
        /// <returns></returns>
        public JobPositionSnapshot Build(Entity job)
        {
            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = job.Id,

                Name = $"{GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, job, null)} {GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, job, null)} (SEQ: {GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, job, null)})",

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, job, null),
                JobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.JobPositionsSeq, job, null),
                StaffJobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, job, null),
                JobPositionDescription = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, job, null),

                // retrieve float values
                AssignmentFte = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.StaffJobPositionFTE, job, null),
                FteRequired = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.JobPositionFTERequired, job, null),

                // retrieve boolean values
                IsActive = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionActiveFlag, job, null),

                // retrieve date times values accounting for null values
                EndDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionEndDate, job, null),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionStartDate, job, null)
            };

            _tracer.Trace("{0} SynMirrorId: {1}", ENTITY_LOGICAL_NAME, snap.SynMirrorId.ToString());

            _tracer.Trace("Calculating job assignment status and status reason");
            _tracer.Trace(
                "SNAP EndDate.HasValue={0}, EndDate={1}, IsActive={2}",
                snap.EndDate.HasValue,
                snap.EndDate.HasValue ? snap.EndDate.Value.ToString("o") : "<null>",
                snap.IsActive
            );
            if (snap.EndDate.HasValue) _tracer.Trace("SNAP EndDate.Value: {0}", snap.EndDate.Value);
            if (
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 1
            )
            {
                _tracer.Trace("Setting status to Inactive");
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                _tracer.Trace("Setting status to Active");
                snap.Status = new OptionSetValue((int)Enums.Status.Active);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

            _tracer.Trace("Calulated status: {0} and statusReason: {1}", snap.Status?.Value, snap.StatusReason?.Value);
=======
            _tracer.Trace("Calulated status: {0} and statusReason: {1}",
                ((Enums.Status)snap.Status?.Value).ToString(),
                ((Enums.StatusReason)snap.StatusReason?.Value).ToString()
            );
            _tracer.Trace("SNAP: {0}",
                JsonSerializer.Serialize(
                    snap,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
>>>>>>> source-temp/refactor/decouple-xrmhelpers

            return snap;
        }
    }
}