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
using System.IO;
using FirstFloor.ModernUI.Presentation;
using System.Runtime.Serialization;
using System.Threading;

namespace Companion_1._0.Content
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>

  
    public partial class SettingsAppearance : UserControl
    {
        SettingsAppearanceViewModel viewModel;
        
       public static string settingspath;
        public SettingsAppearance()
        {
            string directory = Directory.GetCurrentDirectory();
            settingspath = directory + @"/" + "settings.xml";

         
            InitializeComponent();

            viewModel = new SettingsAppearanceViewModel();
          
            if (ReadTheme()!=null)
            {
                Dictionary<string, string> themedictionary = ReadTheme();
                viewModel.SelectedTheme = new Link { DisplayName = "bing image", Source = new Uri(themedictionary["theme"], UriKind.Relative) };
                viewModel.SelectedPalette = themedictionary["palette"];
                viewModel.SelectedAccentColor = (Color)ColorConverter.ConvertFromString(themedictionary["accentcolor"]);
                viewModel.SelectedFontSize = themedictionary["fontsize"];
              
            }
            this.DataContext = viewModel;
        }

        public void SaveTheme()
        {
            try
            {
                File.Delete(settingspath);
                using (FileStream fs = new FileStream(settingspath, FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter sr = new StreamWriter(fs))
                {                    
                    sr.WriteLine(viewModel.SelectedTheme.Source.ToString());
                    sr.WriteLine(viewModel.SelectedAccentColor.ToString());
                    sr.WriteLine(viewModel.SelectedPalette.ToString());
                    sr.WriteLine(viewModel.SelectedFontSize.ToString());
                   
                }
            }
            catch (Exception ex)
            {
                SaveTheme();
            }

        }

        public Dictionary<string,string> ReadTheme()
        {
            Dictionary<string,string> selected=new Dictionary<string, string>();

           
                try
                {
                    using (FileStream fs = new FileStream(settingspath, FileMode.OpenOrCreate, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                    if (String.IsNullOrEmpty(sr.ReadToEnd()))
                    {
                        selected = null;
                    }
                    fs.Position = 0;
                        selected.Add("theme",sr.ReadLine());
                        selected.Add("accentcolor",sr.ReadLine());
                        selected.Add("palette",sr.ReadLine());
                        selected.Add("fontsize",sr.ReadLine());
                    }

                }
                catch (Exception ex)
            {
                selected = null;
            }
           

            return selected;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveTheme();
        }
    }
}
