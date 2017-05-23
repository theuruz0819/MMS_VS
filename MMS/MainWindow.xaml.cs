using System.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
namespace MMS
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        MMG mmg = new MMG();

        public MainWindow()
        {
            InitializeComponent();
            string value = ConfigurationManager.AppSettings["folder_path"];
            folder_select_text_box.Text = value;
        }

        private void folder_select_btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            var path  = dialog.SelectedPath.ToString();
            //ConfigurationManager.AppSettings["folder_path"] = result.ToString();

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["folder_path"] == null)
            {
                settings.Add("folder_path", path);
            }
            else
            {
                settings["folder_path"].Value = path;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            folder_select_text_box.Text = ConfigurationManager.AppSettings["folder_path"];

        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["folder_path"] == null)
            {
                settings.Add("folder_path", folder_select_text_box.Text);
            }
            else
            {
                settings["folder_path"].Value = folder_select_text_box.Text;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            mmg.initital();
        }

        private void process_btn_Click(object sender, RoutedEventArgs e)
        {
            mmg.processFloder();
        }

        private void folder_select_text_box_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
