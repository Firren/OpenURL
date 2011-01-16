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
            string url = urlInput.Text;
            openInBrowser(url);
        }

        private void urlInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string url = urlInput.Text;
                openInBrowser(url);
            }
        }

        private void openInBrowser(String url)
        {
            url = checkURL(url);
            BrowserHandler handler = new BrowserHandler();

            List<Browser> browsers = handler.getInstalledBrowsers();
            for (int i = 0; i < browsers.Count; i++)
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = browsers[i].PathToExe;
                    proc.StartInfo.Arguments = url;
                    proc.Start();
                }
                catch
                {

                }
            }
        }

        private string checkURL(string url)
        {
            url = url.Trim();
            return url.StartsWith(@"http://") ? url : String.Format("http://{0}", url);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            urlInput.Focus();
        }

        


    }
}
