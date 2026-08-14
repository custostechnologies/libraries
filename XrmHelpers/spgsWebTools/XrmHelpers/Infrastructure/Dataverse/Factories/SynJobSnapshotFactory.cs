using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Shared.Hashing;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class SynJobSnapshotFactory
    {
        private readonly ITracingService _tracer;
        private readonly JobSnapshotHashService _hashService;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaffjobpositions";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public SynJobSnapshotFactory(ITracingService tracer)
        {
            _tracer = tracer;
            _hashService = new JobSnapshotHashService(_tracer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="preImage"></param>
        /// <param name="sourceRecordId"></param>
        /// <returns></returns>
        public JobPositionSnapshot Build(Entity target, Entity preImage, Guid sourceRecordId)
        {
            _tracer.Trace("Initialising JobBKDRHashService in JobPositionSnapshot.Build");

            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = sourceRecordId,

                Name = $"{GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage)} {GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage)} (SEQ: {GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage)})",

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage),
                JobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.JobPositionsSeq, target, preImage),
                StaffJobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage),

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

            _tracer.Trace(
                JsonSerializer.Serialize(snap)
            );

            snap.EncodedHash = _hashService.Generate(snap);

            _tracer.Trace("Calculating job assignment status and status reason");

            if (
                // 0 represents inactive
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 0
            )
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Active);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

            _tracer.Trace("Calulated status: {0} and statusReason: {1}", snap.Status, snap.StatusReason);

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

            _tracer.Trace(
                JsonSerializer.Serialize(snap)
            );

            snap.EncodedHash = _hashService.Generate(snap);

            _tracer.Trace("Calculating job assignment status and status reason");

            _tracer.Trace("Calculating job assignment status and status reason");

            if (
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 0
            )
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Active);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

            _tracer.Trace("Calulated status: {0} and statusReason: {1}", snap.Status, snap.StatusReason);

            return snap;
        }
    }
}