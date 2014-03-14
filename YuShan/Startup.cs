using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katana.Controllers;
using Katana.Framework;
using Microsoft.Owin;
using Owin;
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


            app.Get("/", ctx =>
            {
                return "Hello World";
            });

            app.Get("/hello", ctx =>
            {
                var model = new {Title = "Hello Jason"};
                return ctx.Render("helloWorld", model);
            });

            app.Get("/abc/{yourName}/{lastName}", ctx =>
            {
                var param = ctx.Get<IDictionary<string, string>>("param");
                return param["yourName"];
            });

            app.Get("/display_form", async ctx =>
            {  
                 return ctx.Render("post_form");
            });

        }
    }
}
