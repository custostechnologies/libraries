using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Resolvers
{
    public class LinkTypeResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToLabel(int val)
        {
            try
            {
                string result = (uint)val switch
                {
                    384810000 => "Direct Line",
                    384810001 => "Dotted Line",
                    384810002 => "Functional Lead",
                    384810003 => "Discipline Lead",
                    384810004 => "Operational Lead",
                    384810005 => "Financial Lead",
                    _ => "Unknown"
                };

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Resolver error LinkType");
            }
        }
    }
}