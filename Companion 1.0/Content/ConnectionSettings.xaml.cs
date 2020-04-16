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
using ViewModels.BaseClasses.Web_Engine;

namespace Companion_1._0.Content
{
    /// <summary>
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : UserControl
    {
        Options viewModel;
        public ConnectionSettings()
        {
            InitializeComponent();
            rdNoProxy.IsChecked = true;

            viewModel = new Options();
            this.DataContext = viewModel;
            
        }

     

      

        private void ProxyBoxes_Checked(object sender, RoutedEventArgs e)
        {
            if (rdManualProxy.IsChecked == true)
            {
                stP.IsEnabled = true;
                chkUseCred.IsEnabled = true;
                if (chkUseCred.IsChecked == true)
                    gbCred.Visibility = Visibility.Visible;
                else
                    gbCred.Visibility = Visibility.Collapsed;
            }
            else
            {
                stP.IsEnabled = false;
                chkUseCred.IsEnabled = false;
                gbCred.Visibility = Visibility.Collapsed;
            }
           
              
        }
    }
}
