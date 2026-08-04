using System;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Initialisers;
using spgsWebTools.XrmHelpers.Domain.Resolvers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.AssignmentChangeDetection.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class JobAssignmentSnapshotFactory
    {
        private readonly ITracingService _tracer;
        private AssignmentChangeDetectionService _cdService;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaffjobpositions";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public JobAssignmentSnapshotFactory(ITracingService tracer)
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
        public Dtos.JobAssignmentSnapshot Build(Entity target, Entity preImage, Guid sourceRecordId)
        {
            _tracer.Trace("Initialising _cdService in JobAssignmentSnapshot.Build");
            _cdService = new AssignmentChangeDetectionService(_tracer);

            var snap = new Dtos.JobAssignmentSnapshot
            {
                //identity
                SynMirrorId = sourceRecordId,

                Name = GetAttributeDelta.Execute<string>(SynJobPositionFields.JobPositionDescription, target, preImage),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffId, target, preImage),
                StaffJobPositionSeq = GetAttributeDelta.Execute<int>(SynJobPositionFields.StaffJobPositionsSeq, target, preImage),

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

            snap.LongEncodedHash = _cdService.GenerateHash(snap);

            Console.WriteLine("Calculating job assignment status and status reason");

            var (status, statusReason) = StatusResolver.Calculate(snap);

            Console.WriteLine("Calulated status: {0} and statusReason: {1}", status, statusReason);

            snap.Status = status;
            snap.StatusReason = statusReason;

            return snap;
        }
    }
}