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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Companion_1._0.Content;
using System.Windows.Controls.Primitives;
using Companion_1._0.Cwa_Engine;

namespace Companion_1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {

       public static MainWindow mainWindow;
        public static Uri contentSource;

        public static LinkGroup Home;

        public static LinkGroup CwaEngine;
        public static Link CwaEngine_SA;
        public static Link CwaEngine_Analyst;

        public static LinkGroup WebEngine;
        public static Link WebEngine_OfflineBrowser;

        public static LinkGroup Messenger;
        public static Link Messenger_Social;
        public static Link Messenger_Chat;
        public static Link Messenger_Friends;

        public static Link signoutLink;

        public MainWindow()
        {
            InitializeComponent();          
            new SettingsAppearance();
          
            mainWindow = this;
            signoutLink = SignoutLink;
            contentSource = this.ContentSource;

            Home = home;

            CwaEngine = cwaEngine;
            CwaEngine_SA = this.cwaEngine_SA;
            CwaEngine_Analyst = this.cwaEngine_Analyst;

            WebEngine = webEngine;
            WebEngine_OfflineBrowser = webEngine_OfflineBrowser;

            Messenger = messenger;
            Messenger_Social = messenger_Social;
            Messenger_Chat = messenger_Chat;
            Messenger_Friends = messenger_Friends;

           
            
        }

      
    }
}
