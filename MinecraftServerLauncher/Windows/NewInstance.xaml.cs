using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MinecraftServerLauncher.Servers.Versions;

namespace MinecraftServerLauncher.Windows
{
    /// <summary>
    /// Interaction logic for NewInstance.xaml
    /// </summary>
    public partial class NewInstance : Window
    {
        public Project ProjectType { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }

        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public NewInstance()
        {
            InitializeComponent();
            ComboBoxProject.Items.Add(Project.Paper);
            ComboBoxProject.Items.Add(Project.Waterfall);
            ComboBoxProject.Items.Add(Project.Travertine);
            ComboBoxProject.SelectedItem = ComboBoxProject.Items[0];
            Build = "latest";
            _worker.DoWork += Worker_DoWork;
            //_worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void ComboBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxVersions.Items.Clear();

            PopulateVersion();


            if (CheckBoxManual.IsChecked == true)
            {
                PopulateBuilds();
            }
        }

        private void NewInstance_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void ComboBoxVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckBoxManual.IsChecked == true)
            {
                PopulateBuilds();
            }
        }

        private void CheckBoxIsLatest_OnChecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxManual.IsChecked == true)
            {
                PopulateBuilds();
            }
        }

        private void ButtonCreateServer_Click(object sender, RoutedEventArgs e)
        {
            //TODO implement checking if version is downloaded
            if (CheckBoxManual.IsChecked == false)
            {
                Build = "latest";
            }

            Projects.DownloadChangeEvent += UpdateDownload;
            Projects.DownloadCompleted += DownloadCompleted;
            Projects.DownloadJar(ProjectType, Version, Build);
            Debug.WriteLine($"{ProjectType}-{Version}_{Build}");
        }

        private void DownloadCompleted()
        {
            Projects.DownloadChangeEvent -= UpdateDownload;
            Projects.DownloadCompleted -= DownloadCompleted;
        }

        private void UpdateDownload(int progress)
        {
            ProgressBarDownload.Value = progress;
        }

        private void PopulateVersion()
        {
            ProjectType = (Project) ComboBoxProject.SelectedItem;
            foreach (var stringVersion in Projects.GetVersions(ProjectType))
            {
                ComboBoxVersions.Items.Add(stringVersion);
            }

            Version = ComboBoxVersions.Items[0].ToString();
            ComboBoxVersions.SelectedItem = ComboBoxVersions.Items[0];
        }

        private void PopulateBuilds()
        {
            ComboBoxBuilds.Items.Clear();
            Version = ComboBoxVersions.Text;
            string[] builds = Projects.GetBuild(ProjectType, Version);
            if (builds == null)
            {
                ComboBoxBuilds.Items.Add("No builds found");
                return;
            }

            foreach (var build in builds)
            {
                ComboBoxBuilds.Items.Add(build);
            }

            Build = ComboBoxBuilds.Items[0].ToString();
            ComboBoxBuilds.SelectedItem = ComboBoxBuilds.Items[0];
        }
    }
}