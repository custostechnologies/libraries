using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class CoreStudentMapper
    {
        private const string ENTITY_LOGICAL_NAME = EntityLogicalNames.CoreStudent;
        private readonly ITracingService _tracer;
        public CoreStudentMapper(ITracingService tracer)
        {
            _tracer = tracer;
        }
        public Entity ToCreateEntity(CoreStudent core, string[] updatedAttributes)
        {
            _tracer.Trace("ToCreateEntity called with updatedAttributes: {0}", string.Join(",", updatedAttributes));
            var entity = ToEntity(core, true, updatedAttributes);
            return entity;
        }
        public Entity ToUpdateEntity(CoreStudent core, string[] updatedAttributes)
        {
            _tracer.Trace("ToUpdateEntity called with updatedAttributes: {0}", string.Join(",", updatedAttributes));
            if (core.CoreStudentId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = ToEntity(core, false, updatedAttributes);
            entity.Id = core.CoreStudentId;
            return entity;
        }

        public Entity ToEntity(CoreStudent core, bool create, string[] updatedAttributes)
        {
            _tracer.Trace("ToEntity called with core: {0}, create: {1}, and updatedAttributes: {2}",
               JsonSerializer.Serialize(core, new JsonSerializerOptions{WriteIndented=true}),
               create,
               string.Join(",", updatedAttributes)
           );
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity
            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentId,
                CoreStudentFields.StudentId,
                core.StudentId);

            // Status
            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentActiveFlag,
                CoreStudentFields.ActiveFlag,
                core.ActiveFlag);

            _tracer.Trace($"create is: {create}");
            if (create == false)
            {
                MapDelta(entity, updatedAttributes,
                    SynStudentFields.Status,
                    CoreStudentFields.Status,
                    core.Status);
                _tracer.Trace("Mapped core.Status: {0}, Enum: {1}", core.Status.ToString(), ((Enums.Status)core.Status.Value).ToString());

                MapDelta(entity, updatedAttributes,
                    SynStudentFields.StatusReason,
                    CoreStudentFields.StatusReason,
                    core.StatusReason);
                _tracer.Trace("Mapped core.StatusReason: {0}, Enum: {1}", core.StatusReason.ToString(), ((Enums.StatusReason)core.StatusReason.Value).ToString());
            }

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentGiven1,
                CoreStudentFields.Given1,
                core.Given1);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentGiven2,
                CoreStudentFields.Given2,
                core.Given2);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentSurname,
                CoreStudentFields.Surname,
                core.Surname);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentPreferred,
                CoreStudentFields.Preferred,
                core.Preferred);

            // Personal details
            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentHouse,
                CoreStudentFields.House,
                core.House);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentGender,
                CoreStudentFields.Gender,
                core.Gender);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentCampus,
                CoreStudentFields.Campus,
                core.Campus);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.StudentYearLevel,
                CoreStudentFields.YearLevel,
                core.YearLevel);

            MapDelta(entity, updatedAttributes,
                SynStudentFields.Age,
                CoreStudentFields.Age,
                core.Age);

            entity[CoreStudentFields.LastSync] = DateTime.UtcNow;
            _tracer.Trace("Serialised before exiting mapper: {0}",
                JsonSerializer.Serialize(
                    core,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
            _tracer.Trace("Exiting CoreStudentMapper");
            return entity;
        }
        private void MapDelta<T>(
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
            _tracer.Trace("MapDelta sourceAttribute = {0}, targetAttribute = {1}, value = {2}",
                sourceAttribute,
                targetAttribute,
                value
            );

            if (updatedAttributes.Contains(sourceAttribute))
            {
                entity[targetAttribute] = value;
            }

            _tracer.Trace("MapDelta complete, returning to caller.");
        }
    }
}