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

namespace Companion_1._0.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        MainWindow mainWindow;
        public Settings()
        {
            InitializeComponent();
            mainWindow = MainWindow.mainWindow;
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            mainWindow.BackButtonIsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.BackButtonIsEnabled = true;
        }
    }
}
