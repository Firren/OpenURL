using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenURL
{
    class Browser
    {
        private string pathToExe;
        private string name;
        
        public Browser(string name,string pathToExe)
        {
            this.PathToExe = pathToExe;
            this.Name = name;
        }

        public string PathToExe
        {
            get
            {
                return pathToExe;
            }
            set
            {
                pathToExe = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
