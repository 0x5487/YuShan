using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JasonSoft.IO;
using Katana.Framework;
using Microsoft.Owin;
using RazorEngine.Templating;
using YuShan.Framework;

namespace Katana.Controllers
{
    public class Home
    {

        string template = @"<html><head><title></title></head><body>@{ if(3=3){ var myMsg = 5} }@Model.Title, @myMsg</body></html>";
        HelloW myModel = new HelloW() { Title = "Hello World" };
        private TemplateService service = new TemplateService();
       
        

        public Home()
        {

            //service.Compile(template, typeof(HelloW), "HW");
            //myTemplate = Template.Compile(template);


            string viewPath = Path.Combine(@"C:\Users\jason\Documents\Visual Studio 2013\Projects\myKatana\Katana\View\", "helloWorld.cshtml");
            var viewFile = new FileInfo(viewPath);

            string layoutPath = Path.Combine(@"C:\Users\jason\Documents\Visual Studio 2013\Projects\myKatana\Katana\View\", "myLayout.cshtml");
            var layoutFile = new FileInfo(layoutPath);

            if (viewFile.Exists)
            {
                service.GetTemplate(layoutFile.GetText(), null, "myLayout");

                template = viewFile.GetText();
                service.Compile(template, typeof(HelloW), "HW");
            }



            Server.Get("/", async (context) =>
            {
                
                //string template = "<html><head><title></title></head><body>@Model.Title</body></html>";



                //var aTemplate = Template.Compile(template);
                //string content = myTemplate.Render(new { Title = "Hello World" });
                //await context.Response.WriteAsync(content);


                //Template aTemplate = Template.Parse(template);
                //string content = aTemplate.Render(Hash.FromAnonymousObject(new { Title = "Hello World" })); // Renders the output => "hi tobi"
                //await context.Response.WriteAsync(content);


                string content = service.Run("HW", myModel, null);
                await context.Response.WriteAsync(content);
                


                //await context.Render("hello.cshtml", myModel);
            });


            Server.Get("/display_form", async (context) =>
            {

                await context.Render("post_form", null);
            });


            Server.Post("/post_form", async (context) =>
            {
                var form = await context.Request.ReadFormAsync();

                string firstName = form["firstName"];
                string lastnName = form["lastname"];
                string fullName = form["fullName"];

                var model = new { FirstName = firstName, LastName = lastnName, FullName = fullName };

                await context.Render("displayForm.cshtml", model);
            });

            Server.Get("/error", async (context) =>
            {
                throw new NotImplementedException();
            });


            Server.Get("/cookies", async (context) =>
            {
                context.Response.Cookies.Append("jason", "abc", new CookieOptions() { Path = "/" });
            });


            Server.Get("/session", async (context) =>
            {
                object sessionObject = null;
                IDictionary<string, object> session = null;
                if (context.Environment.TryGetValue("YuShan.Session", out sessionObject))
                {
                    session = sessionObject as IDictionary<string, object>;
                }

                session.Add("name", "jason");
            });


            Server.Get("/display_session", async (context) =>
            {
                object sessionObject = null;
                IDictionary<string, object> session = null;
                if (context.Environment.TryGetValue("YuShan.Session", out sessionObject))
                {
                    session = sessionObject as IDictionary<string, object>;
                }

                await context.Response.WriteAsync("Name:" + session["name"].ToString());
            });

        }





    }
}
