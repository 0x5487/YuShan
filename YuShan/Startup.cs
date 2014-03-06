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

            app.Get("/hello", async context =>
            {
                var model = new {Title = "Hello Jason"};
                await context.View<object>("helloWorld", model);
            });

            app.Get("/abc", context =>
            {
                return context.Response.WriteAsync("abc");

            });

            app.Get("/display", async context =>
            {
                await context.View("post_form");  
            });

            app.Use(typeof(NotFoundMiddleware));

        }
    }
}
