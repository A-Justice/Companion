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
using ViewModels.ViewModels;
using System.Threading;
using FirstFloor.ModernUI.Windows.Controls;
using IOops;

namespace Companion_1._0.Pages
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    /// 
    public partial class Registration : UserControl
    {

        ModernWindow mainWindow;
        RegistrationViewModel viewModel;
        public Registration()
        {
            InitializeComponent();
           
            mainWindow = MainWindow.mainWindow;
           viewModel = new RegistrationViewModel();
            this.DataContext = viewModel;
            foreach(var item in CrudOperations.ProgramDictionary.Keys)
            {
                cboProgram.Items.Add(CrudOperations.ProgramDictionary[item]);
            }

            foreach (var item in CrudOperations.LevelDictionary.Keys)
            {
                cboCurrentLevel.Items.Add(CrudOperations.LevelDictionary[item]);
            }

            foreach (var item in CrudOperations.SemesterDictionary.Keys)
            {
                cboCurrentSemester.Items.Add(CrudOperations.SemesterDictionary[item]);
            }

          
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var signal = new ManualResetEvent(false);
            new Thread(() =>
            {
                signal.WaitOne();
                signal.Dispose();
                if (viewModel.verified == true)
                {                    
                    Action action = () => { mainWindow.ContentSource = new Uri("/Pages/HomePage.xaml", UriKind.Relative);
                    };
                    Dispatcher.BeginInvoke(action);
                }
                    
            }).Start();



            new Thread(() =>
            {
                viewModel.SubmitButtonExecute("");
                signal.Set();

            }).Start();
        }

        private void TxtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtErrorEmail.Clear();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
         
            mainWindow.BackButtonIsEnabled = true;
           
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            mainWindow.BackButtonIsEnabled = false;
        }
    }
}
