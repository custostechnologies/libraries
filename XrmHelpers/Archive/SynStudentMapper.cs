using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class SynStudentMapper
    {
        private IDataverseLogger _logger;
        private AttributeCollection _attributes;
        private Entity _target;
        public SynStudentMapper(IDataverseLogger logger, Entity target, AttributeCollection attributes)
        {
            _logger = logger;
            _target = target;
            _attributes = attributes;
        }
        public Entity ToEntity(StudentDto dto)
        {
            _logger.WriteLog("in SynStudentMapper.ToEntity(), _attributes: {0}", string.Join(", ", _attributes.Keys));
            _logger.WriteLog("Hash key discrepancy detected via ChangeDetection plugin for row with Student ID: {0}, New Hash: {1}", dto.StudentId, dto.EncodedHash);
            try
            {
                if (dto.SourceRecordId != Guid.Empty)
                {
                    SetDelta(SynStudentFields.SynMirrorId, dto.SourceRecordId);
                }

                /* SET TARGET DELTAS */
                // identity
                SetDelta(SynStudentFields.StudentId, dto.StudentId);

                // personal details
                SetDelta(SynStudentFields.Age, dto.Age);
                SetDelta(SynStudentFields.StudentGiven1, dto.Given1);
                SetDelta(SynStudentFields.StudentGiven2, dto.Given2);
                SetDelta(SynStudentFields.StudentPreferred, dto.Preferred);
                SetDelta(SynStudentFields.StudentSurname, dto.Surname);
                SetDelta(SynStudentFields.StudentActiveFlag, dto.IsActive);
                SetDelta(SynStudentFields.StudentCampus, dto.Campus);
                SetDelta(SynStudentFields.StudentGender, dto.StudentGender);
                SetDelta(SynStudentFields.StudentYearLevel, dto.StudentYearLevel);

                // status (calculated in snapshot factory)
                SetDelta(SynStudentFields.Status, dto.Status);
                SetDelta(SynStudentFields.StatusReason, dto.StatusReason);
                SetDelta(SynStudentFields.StudentActiveFlag, dto.StudentActiveFlag);

                // sync metadata
                _target[SynStudentFields.EncodedHash] = dto.EncodedHash;
                _target[SynStudentFields.PlaintextHash] = dto.PlaintextHash;
                _target[SynStudentFields.LastSuccessfulSync] = DateTime.UtcNow;

                _logger.WriteLog("Update or create request entity mapped from snapshot");
                _logger.WriteLog("Target attributes set  via StudentChangeDetection plugin for row with Student ID: {0}, New Hash: {1}", dto.StudentId, dto.EncodedHash);
                _logger.WriteLog(
                    "Final encoded hash in target: {0}",
                    _target.GetAttributeValue<int?>("pgrowth_encodedhash")
                );

                _logger.WriteLog("_TARGET AT END OF SynStudentMapper: {0}", JsonSerializer.Serialize(
                    _target,
                    new JsonSerializerOptions { WriteIndented = true }
                ));

                return _target;
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error SynStudentMapper.ToEntity: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("InnerException SynStudentMapper.ToEntity: {0}", ex.InnerException.Message);
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

            _logger.WriteLog("SetDelta _target[{0}] set to: {1}", attributeName, _target.GetAttributeValue<T>(attributeName));
        }
    }
}