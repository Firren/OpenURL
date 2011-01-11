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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string url = trimURL(urlInput.Text);
            openInBrowser(url);
        }

        private void urlInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string url = trimURL(urlInput.Text);
                openInBrowser(url);
            }
        }

        private static string trimURL(string url)
        {
            char[] trimChars = { '\n', '\r' };
            url = url.Trim(trimChars);
            return url;
        }

        private void openInBrowser(String url)
        {
            
            // Applications that we want to run, IE will be handled separately
            string[] apps = { "chrome", "firefox", "safari", "opera" };


            System.Diagnostics.Process[] proc = new System.Diagnostics.Process[apps.Length + 1];


            /******************************* Run IE ************************************/
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            path = path + @"\Internet Explorer\iexplore.exe";
            FileInfo file = new FileInfo(path);

            if (file.Exists)
            {
                proc[apps.Length] = new System.Diagnostics.Process();
                proc[apps.Length].StartInfo.FileName = path;
                proc[apps.Length].StartInfo.Arguments = url;
                try
                {
                    proc[proc.Length - 1].Start();
                }
                catch (Win32Exception ex)
                {
                }
            }
            /************************* Run everything else *****************************/

            for (int i = 0; i < (apps.Length); i++)
            {
                try
                {
                    // First we need to locate our applications exe files
                    path = BrowserPath.find(apps[i]);
                    if (path != null)
                    {
                        // Create process and run application
                        proc[i] = new System.Diagnostics.Process();
                        proc[i].StartInfo.FileName = path;
                        proc[i].StartInfo.Arguments = url;
                        proc[i].Start();
                    }
                }
                catch (Win32Exception ex)
                {

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            urlInput.Focus();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            BrowserHandler handler = new BrowserHandler();
            string test;

            List<Browser> browsers = handler.getInstalledBrowsers();
            for (int i = 0; i < browsers.Count; i++)
            {
                urlInput.Text += browsers[i].Name + " ";
            }
        }


    }
}
