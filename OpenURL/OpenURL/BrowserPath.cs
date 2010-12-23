using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace OpenURL
{
    class BrowserPath
    {
        /**
         * <summary>
         * Tries to find the location of a browsers exe file by its name
         * </summary>
         * <param name="appName">Browsers name</param>
         * <returns>Path to browsers exe file</returns>
         **/
        public static string find(String appName)
        {
            string path = getInstallPathFromRegistry(appName);
            if (path != null)
            {
                path += "\\" + getApplicationExe(path, appName);
                return path;
            }
            return null;
        }

        

        private static string getInstallPathFromRegistry(string pName)
        {
            string displayName;
            RegistryKey key;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;

                if (displayName.ToLower().Contains(pName))
                {
                    return subkey.GetValue("InstallLocation") as String;
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null && displayName.ToLower().Contains(pName))
                {
                    return subkey.GetValue("InstallLocation") as String;
                }
            }



            // NOT FOUND
            return null;
        }

        private static string getApplicationExe(string path, string app)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles("*.exe");
            foreach (FileInfo file in files)
            {
                if (file.Name.ToLower().Contains(app.ToLower()))
                {
                    return file.Name;
                }
            }
            return null;
        }


        
        private static string findByDisplayName(RegistryKey parentKey, string name)
        {
            //BrowserPath.findByDisplayName(Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\"), "iexplore.exe");
            throw new NotImplementedException();
            string[] nameList = parentKey.GetSubKeyNames();
            for (int i = 0; i < nameList.Length; i++)
            {
                RegistryKey regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    if (regKey.Name.ToString().ToLower().Contains(name))
                    {
                        return regKey.GetValue("").ToString();
                    }
                }
                catch { }
            }
            return "";
        }

        
    }
}
