using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class SyncPipeline<TDto> : ISyncPipeline<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseMapper<TDto> _mapper;
        private readonly ILookupEnricher<TDto> _enricher;
        private readonly IChangeDetectionService<TDto> _changeDetector;

        public Entity Execute(Entity target, Entity preImage)
        {
            var previous = _mapper.MapToDto(preImage);
            previous = _enricher.Execute(previous);

            var current = _mapper.MapToDto(target);
            current = _enricher.Execute(current);

            var delta = _changeDetector.Compare(previous, current);

            if (!delta.HasChanged) return null;

            return _mapper.MapToSourceDelta(current, delta);
        }
    }
}