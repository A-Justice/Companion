using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ViewModels.BaseClasses.CwaEngine;
using ViewModels.ViewModels;

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for saData.xaml
    /// </summary>
    
        
    public partial class saData : UserControl
    {
        Cwa_EngineViewModel viewmodel;
       
       
        public saData()
        {
            InitializeComponent();
        }

  

        private void DG_KeyUp(object sender, KeyEventArgs e)
        {
            viewmodel.SubmitExecute("");
            viewmodel.coursess = (ObservableCollection<Courses>)DG.DataContext;          
            DG1.Visibility = Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewmodel = SemesterAverage.cachedViewModel;
            DG.DataContext = viewmodel.coursess;
            DG1.DataContext = viewmodel.output;
        }
    }
}
