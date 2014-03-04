using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasonSoft.IO;
using Microsoft.Owin;
using RazorEngine.Templating;

namespace Katana.Framework
{
    public static class ViewEx
    {

        public static Task Render(this IOwinContext context, string viewPath, object model)
        {
            viewPath = Path.Combine(@"C:\Users\jason\Documents\Visual Studio 2013\Projects\myKatana\Katana\View\", viewPath);
            var viewFile = new FileInfo(viewPath);

            string layoutPath = Path.Combine(@"C:\Users\jason\Documents\Visual Studio 2013\Projects\myKatana\Katana\View\", "myLayout.cshtml");
            var layoutFile = new FileInfo(layoutPath);

            if (viewFile.Exists)
            {
                using (var service = new TemplateService())
                {
                    service.GetTemplate(layoutFile.GetText(), null, "myLayout");

                    string template = viewFile.GetText();
                    string content = service.Parse(template, model, null, null);
                    return context.Response.WriteAsync(content);
                }
            }

            return Task.FromResult(0);
        }
    }
}
