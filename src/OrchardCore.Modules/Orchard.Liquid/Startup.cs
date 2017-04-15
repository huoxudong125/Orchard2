using System.Security.Claims;
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
using System.Collections.Generic;
using System;

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
            Template.RegisterSafeType(typeof(ContentItem), new string[] { nameof(ContentItem.Content) });
            Template.RegisterSafeType(typeof(ContentElement), new string[] { nameof(ContentElement.Content) }, o => (JObject)o);
            Template.RegisterSafeType(typeof(JObject), new string[] { });
            Template.RegisterSafeType(typeof(HttpContext), new string[] { nameof(HttpContext.User) });
            Template.RegisterSafeType(typeof(ClaimsPrincipal), new string[] { nameof(ClaimsPrincipal.Identity) }, o => o.ToString());
            Template.NamingConvention = new CSharpNamingConvention();
        }
    }
}
