using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;

namespace YuShan.ViewEngine
{
    public class RazorViewEngine : IViewEngine
    {
        private static readonly Dictionary<string, string> viewCache = new Dictionary<string, string>();

        public string ViewFolder { get; set; }
        public string LayoutViewName { get; set; }

        public RazorViewEngine()
            : this("Views", "myLayout")
        {
        }

        public RazorViewEngine(string viewFolder, string layoutViewName)
        {
            ViewFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, viewFolder);
            LayoutViewName = layoutViewName;

            if (!Directory.Exists(ViewFolder))
                throw new DirectoryNotFoundException("The view folder specified cannot be located.\r\nThe folder should be in the root of your application which was resolved as " + AppDomain.CurrentDomain.BaseDirectory);


            string content = null;
            var layout = FindView(LayoutViewName);

            if (layout.Exists)
                content = File.ReadAllText(layout.FullName);

            Razor.Compile(content, LayoutViewName);

        }
        private FileInfo FindView(string view)
        {
            var file = new FileInfo(Path.Combine(ViewFolder, view + ".cshtml"));
            if (!file.Exists)
                file = new FileInfo(Path.Combine(ViewFolder, view + ".vbhtml"));

            return file;
        }

        public string Parse(string viewName)
        {
            return Parse<object>(viewName, null);
        }




        public string Parse<T>(string viewName, T model)
        {
            viewName = viewName.ToLower();

            if (!viewCache.ContainsKey(viewName))
            {
                var view = FindView(viewName);

                if (!view.Exists)
                    throw new FileNotFoundException("No view with the name '" + view + "' was found in the views folder (" + ViewFolder + ").\r\nEnsure that you have a file with that name and an extension of either cshtml or vbhtml");

                viewCache[viewName] = File.ReadAllText(view.FullName);
            }

            string result = Razor.Parse(viewCache[viewName], model, viewName);
            return result;
        }
    }
}
