using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenURL
{
    class BrowserHandler
    {
        // Supported browsers
        private static string[] supportedBrowsers = { "chrome", "firefox", "safari", "opera", "iexplore" };

        private List<Browser> installedBrowsers = new List<Browser>();
        
        public BrowserHandler()
        {
            Dictionary<string,string> paths = BrowserPath.find(supportedBrowsers);

            foreach (KeyValuePair<string, string> key in paths)
            {
                installedBrowsers.Add( new Browser(key.Key,key.Value) );
            }
        }

        
        /**
         * <returns>All currently installed browsers</returns>
         **/
        public List<Browser> getInstalledBrowsers()
        {
            return installedBrowsers;
        }
    }
}
