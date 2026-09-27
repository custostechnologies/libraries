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

            foreach (DataverseInjectionTag tag in tags.OfType<DataverseInjectionTag>())
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
        public void PopulateTag<TDto>(
            TDto dto,
            IList<IInjectionTag> tags,
            string token,
            string value
        )
        {
            if (dto == null || token == null) return;
            if (value == null)
            {
                Console.WriteLine($"In PopulateTag, value is null for token: {token}");
                return;
            }

            Console.WriteLine($"PopulateTag with token: {token} and value: {new string(value.Take(25).ToArray())}");

            var tag = tags.FirstOrDefault(
                tag => (
                    tag.InjectionToken == token
                )
            );

            var idx = -1;

            if (tag != null) idx = tags.IndexOf(tag);

            if (idx == -1 || tag == null)
            {
                Console.WriteLine($"tag cannot be found for token: {token}");
                return;
            }

            Console.WriteLine($"tag idx: {idx}");
            Console.WriteLine($"tag InjectionToken: {tag.InjectionToken}");

            tags[idx].Value = value;
        }
    }
}