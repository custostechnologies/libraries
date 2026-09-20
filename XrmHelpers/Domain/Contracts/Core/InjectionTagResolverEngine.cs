using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class InjectionTagResolverEngine : IInjectionTagResolverEngine
    {
        public void PopulateDataverseTags<TDto>(
            TDto dto,
            IList<IInjectionTag> tags
        )
        {
            if (dto == null) return;

            foreach (DataverseInjectionTag tag in tags)
            {
                foreach (var prop in dto.GetType().GetProperties())
                {
                    var link = prop.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;
                    if (link.SourceLogicalName != tag.AttributeLogicalName) continue;

                    var value = prop.GetValue(dto)?.ToString();
                    tag.Value = value;
                }
            }
        }
        public void PopulateDataverseTag<TDto>(
            TDto dto,
            IList<IInjectionTag> tags,
            string token,
            string value
        )
        {
            if (dto == null || token == null) return;

            var idx = tags.IndexOf(
                tags.FirstOrDefault(
                    tag => tag.InjectionToken == token)
            );

            tags[idx].Value = value;
        }
    }
}