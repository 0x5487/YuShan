using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Katana.Controllers;
using Katana.Framework;
using Microsoft.Owin;
using Microsoft.Owin.Diagnostics;
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
                var model = new { Title = "Hello World" };
                return ctx.Render("helloWorld", model);
            });

            app.Get("/google", ctx =>
            {
                ctx.Response.Redirect("http://www.google.com");
                ctx.Response.Write(string.Empty);

            });

            //app.Get("/abc/{firstName}-{lastName}/{fullName}", ctx =>
            //{
            //    var param = ctx.Get<IDictionary<string, string>>("param");

            //    string firstName = null;
            //    if (param.TryGetValue("firstName", out firstName))
            //    {
            //        return firstName;
            //    }
            //    else
            //    {
            //        return new Task<string>("d");
            //    }

            //});

            app.Get("/display_form", async ctx =>
            {   
                 return ctx.Render("post_form");
            });

        }
    }
}
