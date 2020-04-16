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

namespace Companion_1._0.Cwa_Engine
{
    /// <summary>
    /// Interaction logic for EngineWizardStartPage.xaml
    /// </summary>
    public partial class EngineWizardStartPage : UserControl
    {
        public EngineWizardStartPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.ContentSource = new Uri("/Cwa-Engine/MainWindowFrame.xaml", UriKind.Relative);
            
        }
    }
}
