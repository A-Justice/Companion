using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Net;

namespace ViewModels.BaseClasses.Web_Engine
{
    /// <summary>
    /// Options for the downloader
    /// </summary>
    [DataContract]
    public class Options
    {
        #region private varibles

        Uri _downloadUri;
        private DirectoryInfo _destinationFolderPath;
        private bool _stayOnSite;
      
        private int _maximumLinkDepth;
        private string _proxy;
        private bool _proxySet;
        int _port;
        private ProxyUsage _proxyUsage;

        #endregion


        #region publicProperties
        public bool NoProxy { get; set; }

        public bool IndividualFilePath { get; set; }

        public bool SystemProxy { get; set; }

        public bool ManualProxy { get; set; }

        public bool UseCredentials { get; set; }

        public string UserName
        {
            get; set;
        }

        public string PassWord
        {
            get; set;
        }

        [DataMember]
        public int MaximumLinkDepth
        {
            get
            {
                return _maximumLinkDepth;
            }
            set
            {
                _maximumLinkDepth = value;
            }
        }
        [DataMember]
        public DirectoryInfo DestinationFolderPath
        {
            get
            {
                _destinationFolderPath = new DirectoryInfo(Environment.CurrentDirectory + @"/" + "WebEngine" + @"/" + IndividualFilePath);
                if (!_destinationFolderPath.Exists)
                {
                    _destinationFolderPath.Create();
                }

                return _destinationFolderPath;
            }
           
        }

        [DataMember]
        public bool StayOnSite
        {
            get
            {
                return _stayOnSite;
            }
            set
            {
                _stayOnSite = value;
            }
        }

        
        [DataMember]
        public Uri DownloadUri
        {
            get
            {
                return _downloadUri;
            }
            set
            {
                _downloadUri = value;
            }
        }
        [DataMember]
        public bool ProxySet
        {
            get
            {
                return _proxySet;
            }
            set
            {
                _proxySet = value;
            }
        }

       
        [DataMember]
        public WebProxy ProxyAddress
        {
            get
            {
                if (ManualProxy)
                {
                    WebProxy wp = new WebProxy(Proxy.ToString(), _port);
                    if (UseCredentials)
                    {
                        wp.Credentials = new NetworkCredential(UserName, PassWord);
                    }
                    ProxySet = true;
                    return wp;
                    
                }
                else if (SystemProxy)
                {
                    WebProxy wp = (WebProxy)WebRequest.GetSystemWebProxy();
                    ProxySet = true;
                    return wp;

                }
                else
                {
                    ProxySet = false;
                    return null;
                }
                   
               
            }
           
        }

        public string Proxy
        {
            get
            {
                return _proxy;
            }
            set
            {
                _proxy = value;
                if (!string.IsNullOrEmpty(_proxy))
                {
                    ManualProxy = true;
                }
            }
        }
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        

        #endregion


        public Options()
        {
        
        }
    }
}
