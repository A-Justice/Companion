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
    /// Interaction logic for MainWindowFrame.xaml
    /// </summary>
    public partial class MainWindowFrame : UserControl
    {
        public static string WindowName;
        public MainWindowFrame()
        {
            InitializeComponent();
                
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            windowFrame.Content = new PreviousResults();
        }
    }
}
