
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for FirstUse.xaml
    /// </summary>
    public partial class FirstUse : UserControl
    {
        public ModernWindow mainWindow;
        public static FirstUse firstUse ;
        public FirstUse()
        {
            mainWindow = MainWindow.mainWindow;
            InitializeComponent();
            firstUse = this;          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           mainWindow.ContentSource = new Uri("/Pages/Registration.xaml",UriKind.Relative);
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new LoginPage();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.BackButtonIsEnabled = false;
            mainWindow.TitleLinkIsVisible = Visibility.Hidden;
            mainWindow.MenuIsVisible = Visibility.Hidden;
            //if(Visibility == Visibility.Visible)
            //{
            //   // NewlyLoaded = true;
            //    HouseKeeping();
            //}
            //else
            //{
            //    var result = ModernDialog.ShowMessage("Are you sure u want to sign-out","Sign-Out",MessageBoxButton.YesNo);
            //    if(result== MessageBoxResult.No)
            //    {
            //         //new Uri("/Pages/HomePage.xaml",UriKind.Relative);
            //    }
            //    else {
            //        Visibility = Visibility.Visible;
            //        HouseKeeping();
            //    }
            //}
        }

        private void HouseKeeping()
        {
            Action action = new Action(() =>
            {
                mainWindow.BackButtonIsEnabled = false;
                mainWindow.TitleLinkIsVisible = Visibility.Hidden;
                mainWindow.MenuIsVisible = Visibility.Hidden;
            }
         );

            new Thread(() =>
            {
                Dispatcher.BeginInvoke(action);
            }).Start();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
           // Visibility = Visibility.Hidden;
        }
    }
}
