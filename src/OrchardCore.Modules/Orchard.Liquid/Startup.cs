using System.Security.Claims;
using System.Security.Principal;
using DotLiquid;
using DotLiquid.NamingConventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Display.ContentDisplay;
using Orchard.ContentManagement.Handlers;
using Orchard.Data.Migration;
using Orchard.Indexing;
using Orchard.Liquid.Drivers;
using Orchard.Liquid.Handlers;
using Orchard.Liquid.Indexing;
using Orchard.Liquid.Model;
using Microsoft.AspNetCore.Http.Internal;
using System.Linq;

namespace Orchard.Liquid
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddLiquid();

            // Liquid Part
            services.AddScoped<IContentPartDisplayDriver, LiquidPartDisplay>();
            services.AddSingleton<ContentPart, LiquidPart>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IContentPartIndexHandler, LiquidPartIndexHandler>();
            services.AddScoped<IContentPartHandler, LiquidPartHandler>();

            Template.RegisterSafeType(typeof(IContent), new string[] { nameof(IContent.ContentItem) });
            Template.RegisterSafeType(typeof(ContentItem), new string[] {
                nameof(ContentItem.Id),
                nameof(ContentItem.ContentItemId),
                nameof(ContentItem.Number),
                nameof(ContentItem.Owner),
                nameof(ContentItem.Author),
                nameof(ContentItem.Published),
                nameof(ContentItem.Latest),
                nameof(ContentItem.ContentType),
                nameof(ContentItem.CreatedUtc),
                nameof(ContentItem.ModifiedUtc),
                nameof(ContentItem.PublishedUtc)
            });

            Template.RegisterSafeType(typeof(ContentElement), new string[] {
                nameof(ContentElement.Content),
                nameof(ContentElement.ContentItem)
            });
            Template.RegisterSafeType(typeof(DefaultHttpContext), new string[] {
                nameof(HttpContext.User),
                nameof(HttpContext.Request)
            });
            Template.RegisterSafeType(typeof(DefaultHttpRequest), new string[] {
                nameof(HttpRequest.Path),
                nameof(HttpRequest.PathBase),
                nameof(HttpRequest.Host),
                nameof(HttpRequest.IsHttps),
                nameof(HttpRequest.Method),
                nameof(HttpRequest.Protocol),
                nameof(HttpRequest.Query),
                nameof(HttpRequest.QueryString),
                nameof(HttpRequest.Form),
                nameof(HttpRequest.Cookies),
                nameof(HttpRequest.Headers)
            });

            Template.RegisterSafeType(typeof(HostString), p => p.ToString());
            Template.RegisterSafeType(typeof(PathString), p => p.ToString());
            Template.RegisterSafeType(typeof(QueryString), p => p.ToString());
            Template.RegisterSafeType(typeof(IQueryCollection), p => ((IQueryCollection)p).ToDictionary(x => x.Key, y => y.Value));
            Template.RegisterSafeType(typeof(IFormCollection), p => ((IFormCollection)p).ToDictionary(x => x.Key, y => y.Value));
            Template.RegisterSafeType(typeof(IRequestCookieCollection), p => ((IRequestCookieCollection)p).ToDictionary(x => x.Key, y => y.Value));
            Template.RegisterSafeType(typeof(IHeaderDictionary), p => ((IHeaderDictionary)p).ToDictionary(x => x.Key, y => y.Value));

            Template.RegisterSafeType(typeof(IPrincipal), new string[] { nameof(IPrincipal.Identity) });
            Template.RegisterSafeType(typeof(IIdentity), new string[] { nameof(IIdentity.Name) });

            DotLiquid.Liquid.UseRubyDateFormat = false;

            Template.NamingConvention = new CSharpNamingConvention();
        }
    }
}
