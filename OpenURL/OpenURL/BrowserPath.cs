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
            string[] appsArray = { appName };
            Dictionary<string,string> apps = find(appsArray);
            string path = null;
            if (apps.TryGetValue(appName, out path))
            {
                return path;
            }
            return null;
        }
        /**
         * <summary>
         * Tries to find the location of browsers exe files by the browsers name
         * </summary>
         * <param name="appName">Browsers name</param>
         * <returns>A dictionary with the browsers name as key and the path to the executable as the value</returns>
         **/
        public static Dictionary<string,string> find(String[] appName)
        {
            Dictionary<string,string> path = getInstallPathFromRegistry(appName);
            string[] keys = path.Keys.ToArray();
            foreach (string key in keys)
            {
                path[key] += "\\" + getApplicationExe(path[key], key);
            }
            return path;
        }

        private static Dictionary<string,string> getInstallPathFromRegistry(string[] pName)
        {
            Dictionary<string,string> installPath = new Dictionary<string,string>();
                string displayName;
                RegistryKey key;
                
                // search in: CurrentUser
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (string name in pName)
                {
                    if (name.Equals("iexplore"))
                    {
                        if (!installPath.ContainsKey(name))
                        {
                            String installLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                            installLocation += @"\Internet Explorer";
                            installPath.Add(name, installLocation);
                            continue;
                        }
                    }
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey.GetValue("DisplayName") as string;
                    
                        if (displayName.ToLower().Contains(name))
                        {
                            string installLocation = subkey.GetValue("InstallLocation") as string;
                            if (!installPath.ContainsKey(name))
                            {
                                installPath.Add(name, installLocation);
                                break;
                            }
                        }
                    
                    }
                }

                // search in: LocalMachine_32
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (string name in pName)
                {
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey.GetValue("DisplayName") as string;
                    
                        if (displayName != null && displayName.ToLower().Contains(name))
                        {
                            string installLocation = subkey.GetValue("InstallLocation") as string;
                            if (!installPath.ContainsKey(name))
                            {
                                installPath.Add(name, installLocation);
                                break;
                            }
                        }
                    }
                }
                return installPath;
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
        // Not implemented
        /**
         * <summary>Find application by display name</summary>
         **/
        private static string findByDisplayName(RegistryKey parentKey, string name)
        {
            //BrowserPath.findByDisplayName(Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\"), "iexplore.exe");
            throw new NotImplementedException();
            //string[] nameList = parentKey.GetSubKeyNames();
            //for (int i = 0; i < nameList.Length; i++)
            //{
            //    RegistryKey regKey = parentKey.OpenSubKey(nameList[i]);
            //    try
            //    {
            //        if (regKey.Name.ToString().ToLower().Contains(name))
            //        {
            //            return regKey.GetValue("").ToString();
            //        }
            //    }
            //    catch { }
            //}
            //return "";
        }

        
    }
}
