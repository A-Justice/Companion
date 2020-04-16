using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ViewModels.BaseClasses;
using ViewModels.ViewModels;

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for SemesterAverage.xaml
    /// </summary>
    public partial class SemesterAverage : UserControl
    {
        Cwa_EngineViewModel viewModel;
        public static Cwa_EngineViewModel cachedViewModel;
        public static ModernFrame Frame;
       
        public SemesterAverage()
        {
            InitializeComponent();
            ResetViewModel();
            Frame = this.frame;
            viewModel = new Cwa_EngineViewModel();


            foreach (var item in CrudOperations.ProgramDictionary.Keys)
            {
                cboProgram.Items.Add(CrudOperations.ProgramDictionary[item]);
            }

            foreach (var item in CrudOperations.LevelDictionary.Keys)
            {
                cboLevel.Items.Add(CrudOperations.LevelDictionary[item]);
            }

            foreach (var item in CrudOperations.SemesterDictionary.Keys)
            {
                cboSemester.Items.Add(CrudOperations.SemesterDictionary[item]);
            }

            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ResetViewModel();
            //frame.Content = new saData();
            frame.Source = new Uri("/Cwa-Engine/saData.xaml", UriKind.Relative);
       
        }

        public void ResetViewModel()
        {
            DataContext = viewModel;
            cachedViewModel = (Cwa_EngineViewModel)DataContext;
        }
    }
}
