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
        public JobPositionSnapshot Build(Entity target, Entity preImage, Guid sourceRecordId)
        {
            _tracer.Trace("Initialising JobBKDRHashService in JobPositionSnapshot.Build");

            JobBKDRHashService _hashService = new JobBKDRHashService(_tracer);

            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = sourceRecordId,

                Name = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage),

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
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 1
            )
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive); snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Active); snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
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
            _tracer.Trace("Initialising JobBKDRHashService in JobPositionSnapshot.Build");

            JobBKDRHashService _hashService = new JobBKDRHashService(_tracer);

            _tracer.Trace("Creating job snapshot in JobPositionSnapshot.Build");

            var snap = new JobPositionSnapshot
            {
                //identity
                SynMirrorId = job.Id,

                Name = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, job),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, job),
                JobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.JobPositionsSeq, job),
                StaffJobPositionsSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, job),

                // retrieve float values
                AssignmentFte = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.StaffJobPositionFTE, job),
                FteRequired = GetAttributeDelta.Execute<decimal>(SynJobPositionFields.JobPositionFTERequired, job),

                // retrieve boolean values
                IsActive = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionActiveFlag, job),

                // retrieve date times values accounting for null values
                EndDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionEndDate, job),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynJobPositionFields.StaffJobPositionStartDate, job)
            };

            _tracer.Trace("{0} SynMirrorId: {1}", ENTITY_LOGICAL_NAME, snap.SynMirrorId.ToString());

            _tracer.Trace(
                JsonSerializer.Serialize(snap)
            );

            snap.EncodedHash = _hashService.Generate(snap);

            _tracer.Trace("Calculating job assignment status and status reason");

            _tracer.Trace("Calculating job assignment status and status reason");

            if (
                (snap.EndDate.HasValue && snap.EndDate.Value <= DateTime.UtcNow) || snap.IsActive == 1
            )
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive); snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Active); snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Active);
            }

            _tracer.Trace("Calulated status: {0} and statusReason: {1}", snap.Status, snap.StatusReason);

            return snap;
        }
    }
    public class JobBKDRHashService
    {
        private readonly ITracingService _tracer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public JobBKDRHashService(ITracingService tracer)
        {
            _tracer = tracer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="snap"></param>
        /// <returns></returns>

        public int Generate(JobPositionSnapshot snap)
        {

            string plaintextHash = string.Join("|",
                new string?[] {
                    snap.Name ?? "",
                    snap.JobPositionsSeq.ToString(),
                    snap.StaffJobPositionsSeq.ToString(),
                    snap.SynReportsTo ?? "",
                    snap.AssignmentFte.ToString(),
                    snap.FteRequired.ToString(),
                    snap.StaffId.ToString(),
                    snap.IsActive.ToString(),
                    snap.StartDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                    snap.EndDate?.ToString("yyyy-MM-dd HH:mm:ss")
                }.Select(val => val ?? string.Empty)
            );

            _tracer.Trace("Plaintext hash: {0}", plaintextHash);

            snap.PlaintextHash = plaintextHash;

            return GetBKDRHashKey.Calculate(plaintextHash);
        }
    }
}