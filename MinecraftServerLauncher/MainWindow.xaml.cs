using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MinecraftServerLauncher.Settings;
using MinecraftServerLauncher.Windows;

namespace MinecraftServerLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public NewInstance NewInstance { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void ButtonCreateInstance_Click(object sender, RoutedEventArgs e)
        {
            if (NewInstance == null)
            {
                NewInstance = new NewInstance();
            }

            NewInstance.Visibility = Visibility.Visible;
        }
    }
}
