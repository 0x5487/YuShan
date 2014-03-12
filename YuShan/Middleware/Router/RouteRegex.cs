using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Owin.HelloWorld.Routing;
using YuShan.Middlewares;
using JasonSoft;


namespace Owin.HelloWorld.Routing
{
    internal class RouteRegex
    {
        private static readonly Regex paramRegex = new Regex(@"{(?<name>[A-Za-z0-9_]*)}", RegexOptions.Compiled);
        private string _route;
        private Regex _routeRegex;
        private IList<string> _keys;
        public IDictionary<string, string> Parameters { get; set; }

        private Regex RouteToRegex(string route)
        {
            var parts = route.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();

            parts = parts.Select(part => !paramRegex.IsMatch(part) ?
                part :
                string.Join("",
                    paramRegex.Matches(part)
                        .Cast<Match>()
                        .Where(match => match.Success)
                        .Select(match => {
                            string key = match.Groups["name"].Value;
                            _keys.Add(key);
                            return string.Format("(?<{0}>.+?)", key.Replace(".", @"\."));
                        })
                    )
                );

            return new Regex("^/" + string.Join("/", parts) + "$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool Validate(string path) 
        {
            bool isMatch = _routeRegex.IsMatch(path);

            if (isMatch) 
            {
                var groups = _routeRegex.Matches(path).Cast<Match>().Where(match => match.Success);

                if (!groups.IsNullOrEmpty() && !_keys.IsNullOrEmpty())
                {
                    Parameters = new Dictionary<string, string>();

                    foreach (var key in _keys) 
                    {
                        string value = groups.Select(match => match.Groups[key].Value).SingleOrDefault();
                        Parameters.Add(key, value);        
                    }
                }
            }

            return isMatch;
        }


        public RouteRegex(string route) 
        {
            this._keys = new List<string>();
            this._route = route;

            this._routeRegex = RouteToRegex(_route);
        }
    }
}
