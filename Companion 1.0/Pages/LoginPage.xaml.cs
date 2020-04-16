
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
using ViewModels.ViewModels;

namespace Companion_1._0.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : ModernDialog
    {
        LoginViewModel viewModel;
        ModernWindow mainWindow;
        public LoginPage()
        {
            InitializeComponent();

            
              
            

            viewModel = new LoginViewModel();
            this.DataContext = viewModel;
            OkButton.Width = 80;
            OkButton.Height = 30;
            OkButton.Content = "Login";
            

            
            
            CancelButton.Height = 30;
            CancelButton.Width = 80;


            mainWindow = MainWindow.mainWindow;
            OkButton.Command = viewModel.LoginCommand;
            OkButton.Click += LoginButton_Click;

            Buttons = null;
            Buttons = new List<Button>() {OkButton, CancelButton };
                                   
            ShowDialog();
                    
        }

        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
           
            mainWindow.ContentSource = new Uri("/Pages/HomePage.xaml", UriKind.Relative);
            this.Close();
        }
    }
}
