using DotLiquid;
using System.Collections.Concurrent;

namespace Orchard.Liquid
{
    public class LiquidManager : ILiquidManager
    {
        private static Template Empty = Template.Parse("");

        private static ConcurrentDictionary<string, Template> _templates = new ConcurrentDictionary<string, Template>();
        
        public Template GetTemplate(string source)
        {
            if (_templates.TryGetValue(source, out var template))
            {
                return template;
            }
            else
            {
                try
                {
                    template = Template.Parse(source);
                    template = _templates.GetOrAdd(source, template);
                    return template;
                }
                catch
                {
                    return Empty;
                }
            }

            
        }
    }
}
