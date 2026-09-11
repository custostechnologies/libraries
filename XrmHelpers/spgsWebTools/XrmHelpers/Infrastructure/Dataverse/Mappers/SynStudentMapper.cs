using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class SynStudentMapper
    {
        private ITracingService _tracer;
        private AttributeCollection _attributes;
        private Entity _target;
        public SynStudentMapper(ITracingService tracer, Entity target, AttributeCollection attributes)
        {
            _tracer = tracer;
            _target = target;
            _attributes = attributes;
        }
        public Entity ToEntity(StudentSnapshot snap)
        {
            _tracer.Trace("in SynStudentMapper.ToEntity(), _attributes: {0}", string.Join(", ", _attributes.Keys));
            _tracer.Trace("Hash key discrepancy detected via ChangeDetection plugin for row with Student ID: {0}, New Hash: {1}", snap.StudentId, snap.EncodedHash);
            try
            {
                if (snap.SourceRecordId != Guid.Empty)
                {
                    SetDelta(SynStudentFields.SynMirrorId, snap.SourceRecordId);
                }

                /* SET TARGET DELTAS */
                // identity
                SetDelta(SynStudentFields.StudentId, snap.StudentId);

                // personal details
                SetDelta(SynStudentFields.Age, snap.Age);
                SetDelta(SynStudentFields.StudentGiven1, snap.StudentGiven1);
                SetDelta(SynStudentFields.StudentGiven2, snap.StudentGiven2);
                SetDelta(SynStudentFields.StudentPreferred, snap.StudentPreferred);
                SetDelta(SynStudentFields.StudentSurname, snap.StudentSurname);
                SetDelta(SynStudentFields.StudentActiveFlag, snap.StudentActiveFlag);
                SetDelta(SynStudentFields.StudentCampus, snap.StudentCampus);
                SetDelta(SynStudentFields.StudentGender, snap.StudentGender);
                SetDelta(SynStudentFields.StudentYearLevel, snap.StudentYearLevel);

                // status (calculated in snapshot factory)
                SetDelta(SynStudentFields.Status, snap.Status);
                SetDelta(SynStudentFields.StatusReason, snap.StatusReason);
                SetDelta(SynStudentFields.StudentActiveFlag, snap.StudentActiveFlag);

                // sync metadata
                _target[SynStudentFields.EncodedHash] = snap.EncodedHash;
                _target[SynStudentFields.PlaintextHash] = snap.PlaintextHash;
                _target[SynStudentFields.LastSuccessfulSync] = DateTime.UtcNow;

                _tracer.Trace("Update or create request entity mapped from snapshot");
                _tracer.Trace("Target attributes set  via StudentChangeDetection plugin for row with Student ID: {0}, New Hash: {1}", snap.StudentId, snap.EncodedHash);
                _tracer.Trace(
                    "Final encoded hash in target: {0}",
                    _target.GetAttributeValue<int?>("pgrowth_encodedhash")
                );

                _tracer.Trace("_TARGET AT END OF SynStudentMapper: {0}", JsonSerializer.Serialize(
                    _target,
                    new JsonSerializerOptions { WriteIndented = true }
                ));

                return _target;
            }
            catch (Exception ex)
            {
                _tracer.Trace("Error SynStudentMapper.ToEntity: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("InnerException SynStudentMapper.ToEntity: {0}", ex.InnerException.Message);
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

            _tracer.Trace("SetDelta _target[{0}] set to: {1}", attributeName, _target.GetAttributeValue<T>(attributeName));
        }
    }
}