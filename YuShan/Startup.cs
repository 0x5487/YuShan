using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katana.Controllers;
using Katana.Framework;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;
using Owin.HelloWorld.ViewEngine;
using YuShan.Middlewares;
using YuShan.Session.Owin;
using YuShan.Routing;
using YuShan.ViewEngine;

namespace YuShan
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif

            app.DefaultViewEngine<RazorViewEngine>();
            //app.Use(typeof (InStoreSessionMiddleware));

            //new Home();
            //app.Use(typeof (RouteMiddleware));

            app.Get("/", context =>
            {
                return context.Response.WriteAsync("Thank You");

            });

            app.Get("/hello", context =>
            {
                var model = new {Title = "Hello Jason"};
                return context.View<object>("helloWorld", model);
            });

            app.Get("/abc/{yourName}/{lastName}", ctx =>
            {
                var param = ctx.Get<IDictionary<string, string>>("param");

                return ctx.Response.WriteAsync(param["yourName"]);

            });

            app.Get("/display_form", async context =>
            {
                await context.View("post_form");  
            });

            app.Use(typeof(NotFoundMiddleware));

        }
    }
}
