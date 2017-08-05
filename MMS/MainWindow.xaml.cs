using System.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using MMS.Data;

namespace MMS
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        MMG mmg = new MMG();
        DataBase sqliteDb = new DataBase();
        List<Tag> allTags;
        List<Tag> selectedTags = new List<Data.Tag>();
        private List<Manga> comicList;
        private List<Manga> selectedComicList = new List<Manga>();
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

            allTags = sqliteDb.getAllTags();
            tag_list.ItemsSource = allTags;

        }

        private void process_btn_Click(object sender, RoutedEventArgs e)
        {
            mmg.processFloder();
            //testing database
            //sqliteDb.selectQuery("SELECT * FROM TEST_TABLE");
            //Console.WriteLine("db test finish");

            // testing data binding
            /*
            List<Tag> tags = new List<Tag>();
            tags.Add(new Tag() { name = "tag1"});
            tags.Add(new Tag() { name = "tag2" });
            tags.Add(new Tag() { name = "tag3" });
            tags.Add(new Tag() { name = "tag4" });
            tag_list.ItemsSource = tags;
            */
        }

        private void folder_select_text_box_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void tag_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void tag_list_double_click(object sender, EventArgs e) {
            if (tag_list.SelectedItem != null)
            {
                //System.Windows.MessageBox.Show(allTags[tag_list.SelectedIndex].name);
                if (!selectedTags.Contains(allTags[tag_list.SelectedIndex])) {
                    selectedTags.Add(allTags[tag_list.SelectedIndex]);
                    selected_tag_list_box.ItemsSource = selectedTags;
                    selected_tag_list_box.Items.Refresh();
                    comicList = sqliteDb.getQueryMangaAndSelection(selectedTags);
                    comic_list.ItemsSource = comicList;
                    selected_tag_list_box.Items.Refresh();
                }

            }
        }

        private void selected_tag_list_double_click(object sender, EventArgs e)
        {
            if (tag_list.SelectedItem != null)
            {
                //System.Windows.MessageBox.Show(allTags[tag_list.SelectedIndex].name);
                selectedTags.RemoveAt(selected_tag_list_box.SelectedIndex);
                selected_tag_list_box.ItemsSource = selectedTags;
                selected_tag_list_box.Items.Refresh();
                
                comicList = sqliteDb.getQueryMangaAndSelection(selectedTags);
                comic_list.ItemsSource = comicList;
                selected_tag_list_box.Items.Refresh();
            }
        }
        private void selected_tag_list_box_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void comic_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void comic_list_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (comic_list.SelectedItem != null)
            {   if (!selectedComicList.Contains(comicList[comic_list.SelectedIndex]))
                {
                    selectedComicList.Add(comicList[comic_list.SelectedIndex]);
                    selected_comic_list.ItemsSource = selectedComicList;
                    selected_comic_list.Items.Refresh();
                }
            }
        }

        private void selected_comic_list_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selected_comic_list.SelectedItem != null) {
                selectedComicList.RemoveAt(selected_comic_list.SelectedIndex);
                selected_comic_list.ItemsSource = selectedComicList;
                selected_comic_list.Items.Refresh();
            }
        }

        private void add_tag_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void copy_to_process_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Manga comic in selectedComicList) {
                mmg.DirectoryCopy(comic.path, comic.title);
                copy_progress_bar.Value = copy_progress_bar.Value + 1;
            }
            System.Windows.MessageBox.Show("COPY COMPLETED");
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
