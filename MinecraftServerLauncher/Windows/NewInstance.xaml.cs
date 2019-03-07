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
        public NewInstance()
        {
            InitializeComponent();
            ComboBoxProject.Items.Add(Project.Paper);
            ComboBoxProject.Items.Add(Project.Waterfall);
            ComboBoxProject.Items.Add(Project.Travertine);
            ComboBoxProject.SelectedItem = ComboBoxProject.Items[2];
        }

        private void NewInstance_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void ComboBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxVersions.Items.Clear();
            foreach (var stringVersion in Projects.GetVersions((Project)ComboBoxProject.SelectedItem))
            {
                ComboBoxVersions.Items.Add(stringVersion);
            }

            if (ComboBoxVersions.Items.Count > 0)
            {
                ComboBoxVersions.SelectedItem = ComboBoxVersions.Items[0];
            }

            if (CheckBoxManual.IsChecked == true)
            {
                CheckBoxIsLatest_OnChecked(null, null);
            }
        }

        private void CheckBoxIsLatest_OnChecked(object sender, RoutedEventArgs e)
        {
            ComboBoxBuilds.Items.Clear();
            string[] builds = Projects.GetBuild((Project) ComboBoxProject.SelectedItem, ComboBoxVersions.Text);
            if (builds == null)
            {
                ComboBoxBuilds.Items.Add("No builds found");
                return;
            }
            foreach (var build in builds)
            {
                ComboBoxBuilds.Items.Add(build);
            }

            ComboBoxBuilds.SelectedItem = ComboBoxBuilds.Items[0];
        }
    }
}
