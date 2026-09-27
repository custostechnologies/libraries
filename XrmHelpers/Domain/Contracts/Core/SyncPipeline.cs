using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class SyncPipeline<TDto> : ISyncPipeline<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        private readonly IDataverseMapper<TDto> _mapper;
        private readonly ILookupEnricher<TDto> _enricher;
        private readonly IChangeDetectionService<TDto> _changeDetector;
        public SyncPipeline(
            IDataverseContext context,
            IDataverseMapper<TDto> mapper,
            ILookupEnricher<TDto> enricher,
            IChangeDetectionService<TDto> changeDetector
        )
        {
            _context = context;
            _mapper = mapper;
            _enricher = enricher;
            _changeDetector = changeDetector;

            _logger = _context.GetLogger<SyncPipeline<TDto>>();
        }
        // MAP SOURCE -> SOURCE
        public Entity ExecuteInbound(Entity target, Entity? preImage)
        {
            DeltaComparison<TDto>? delta = null;
            TDto current;
            TDto previous;

            try
            {
                _logger.WriteLog("Mapping target...");
                current = _mapper.MapToDto(target);

                _logger.WriteLog("Enriching target...");
                current = _enricher.Execute(current);

                if (preImage == null)
                {
                    _logger.WriteLog("preImage is NULL, creating new record instead.");
                    return _mapper.MapToSource(current);
                }
                else
                {
                    _logger.WriteLog("Mapping preImage...");
                    previous = _mapper.MapToDto(preImage);

                    _logger.WriteLog("Enriching preImage...");
                    previous = _enricher.Execute(previous);

                    delta = _changeDetector.Compare(previous, current);

                    _logger.WriteLog(
                        $"Change detection completed. " +
                        $"HasChanged={delta.HasChanged}; ");
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: {ex}");
                throw;
            }

            if (delta == null || !delta.HasChanged)
            {
                return new Entity();
            }
            else
            {
                return _mapper.MapToSourceDelta(current, delta);
            }
        }

        // MAP SOURCE -> TARGET
        public Entity ExecuteOutbound(Entity target)
        {
            TDto dto;

            try
            {
                _logger.WriteLog("Mapping target...");
                dto = _mapper.MapToDto(target);

                _logger.WriteLog("Enriching target...");
                dto = _enricher.Execute(dto);

                return _mapper.MapToTarget(dto);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: {ex}");
                throw;
            }
        }
    }
}