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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        MainWindow mainWindow;
        public HomePage()
        {
            InitializeComponent();
            mainWindow = MainWindow.mainWindow;
            MainWindow.Home.DisplayName = "Home";

            MainWindow.CwaEngine.DisplayName = "CWA-Engine";
            MainWindow.CwaEngine_SA.DisplayName = "Calculate Semester Average";
            MainWindow.CwaEngine_Analyst.DisplayName = "CWA Analyst";


            MainWindow.WebEngine.DisplayName = "WebEngine";
            MainWindow.WebEngine_OfflineBrowser.DisplayName = "Offline-Browser";

            MainWindow.Messenger.DisplayName = "Messenger";
            MainWindow.Messenger_Chat.DisplayName = "Chat";
            MainWindow.Messenger_Friends.DisplayName = "Friends";
            MainWindow.Messenger_Social.DisplayName = "Social";

           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.BackButtonIsEnabled = false;
            mainWindow.TitleLinkIsVisible = Visibility.Visible;
            mainWindow.MenuIsVisible = Visibility.Visible;
           
        }
    }
}
