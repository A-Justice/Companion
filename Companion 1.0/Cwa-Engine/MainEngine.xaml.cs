using Companion_1._0.Pages;
using IOops;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for MainEngine.xaml
    /// </summary>
    public partial class MainEngine : UserControl
    {
        Student CurrentStudent = null;
        public MainEngine()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (RegistrationViewModel.CurrentStudent != null)
            {
                CurrentStudent = RegistrationViewModel.CurrentStudent;
                new LoginViewModel();
                LoginViewModel.CurrentStudent = CurrentStudent;
            }
            else
                CurrentStudent = LoginViewModel.CurrentStudent;

            if (CurrentStudent.CurrentLevel == Level.L100 && CurrentStudent.CurrentSemester == Semester.S1)
                File.Create(CurrentStudent.PersonalDirectory + @"/PreviousRecords.xml").Dispose();

            if (File.Exists(CurrentStudent.PersonalDirectory + @"/PreviousRecords.xml"))
            {
                windowFrame.Content = new CwaAnalyst();
            }
            else
                MainWindow.mainWindow.ContentSource = new Uri("/Cwa-Engine/EngineWizardStartPage.xaml", UriKind.Relative);

        }
    }
}
