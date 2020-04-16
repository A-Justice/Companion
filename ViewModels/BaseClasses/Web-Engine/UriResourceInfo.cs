using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    [DataContract]
   public class UriResourceInfo
    {
        #region privateVariables

        private readonly Options _options;
        private readonly string _originalUrl;

        private readonly Uri _relativeUri = null;
        private readonly Uri _baseUri = null;
        private readonly Uri _absoluteUri = null;

        private readonly UriType _linkType = UriType.Content;
        private UriType? _calculatedLinkType = null;

        #endregion

        #region PublicProperties
        /// <summary>
		/// Gets the original URL.
		/// </summary>
		/// <value>The original URL.</value>
		public string OriginalUrl
        {
            get
            {
                return _originalUrl;
            }
        }

        /// <summary>
        /// Gets the relative URI.
        /// </summary>
        /// <value>The relative URI.</value>
        public Uri RelativeUri
        {
            get
            {
                return _relativeUri;
            }
        }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        public Uri BaseUri
        {
            get
            {
                return _baseUri;
            }
        }

        public Uri BaseUriWithFolder
        {
            get
            {
                if (_absoluteUri.AbsoluteUri.Contains(@"/"))
                {
                    string full =
                        _absoluteUri.AbsoluteUri.Substring(0, _absoluteUri.AbsoluteUri.LastIndexOf('/'));
                    return new Uri(full, UriKind.RelativeOrAbsolute);
                }
                else
                    return _baseUri;
            }
        }

        public Uri AbsoluteUri
        {
            get
            {
                return _absoluteUri;
            }
        }

        /// <summary>
        /// Gets the type of the link.
        /// </summary>
        /// <value>The type of the link.</value>
        public UriType LinkType
        {
            get
            {
                if (_calculatedLinkType == null)
                {
                    _calculatedLinkType =
                        CheckVerifyLinkType(_absoluteUri, _linkType);
                }

                return _calculatedLinkType.Value;
            }
        }

        /// <summary>
        /// Decides whether to follow an URI.
        /// </summary>
        /// <value><c>true</c> if [want follow URI]; otherwise, <c>false</c>.</value>
        public bool WantFollowUri
        {
            get
            {
                return DoWantFollowUri(
                    _absoluteUri,
                    _linkType);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is resource URI.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is resource URI; otherwise, <c>false</c>.
        /// </value>
        public bool IsResourceUri
        {
            get
            {
                return DoIsResourceUri(_absoluteUri);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is processable URI.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is processable URI; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessableUri
        {
            get
            {
                return
                    !_originalUrl.StartsWith(@"#") &&
                    DoIsProcessableUri(_absoluteUri, _linkType);
            }
        }

        #endregion

        #region publicMethods
        public UriResourceInfo(Options options,string originalUrl
            ,Uri uri,Uri baseUri,UriType linkType)
        {
            _options = options;
            _originalUrl = originalUrl;
            _baseUri = baseUri;

            uri = new Uri(CleanupUrl(uri.OriginalString), UriKind.Relative);

            if(Uri.IsWellFormedUriString(uri.OriginalString,UriKind.Absolute))
            {
                _absoluteUri = uri;
                _relativeUri = null;
            }
            else if(Uri.IsWellFormedUriString(uri.OriginalString,UriKind.Relative))
            {
                _absoluteUri = MakeAbsoluteUri(baseUri, uri);
                _relativeUri = uri;
            }
            else
            {
                if (originalUrl.StartsWith(@"#"))
                {
                    _absoluteUri = null;
                    _relativeUri = new Uri(originalUrl, UriKind.Relative);
                }
                else
                {
                    _absoluteUri = MakeAbsoluteUri(baseUri, uri);
                    _relativeUri = uri;
                }
                _linkType = linkType;
            }
        }

        public UriResourceInfo(UriResourceInfo copyFrom)
        {
            _options = copyFrom._options;
            _originalUrl = copyFrom._originalUrl;
            _relativeUri = copyFrom._relativeUri;
            _baseUri = copyFrom._baseUri;
            _absoluteUri = copyFrom._absoluteUri;
            _linkType = copyFrom._linkType;
        }

        public bool IsOnSameSite(Uri uri)
        {
            if(string.Compare(_absoluteUri.Host,uri.Host,true) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region PrivateMethods

        /// <summary>
        /// checks the type of the verify link.
        /// </summary>
        /// <param name="absoluteUri">The absolute URI</param>
        /// <param name="linkType">type of the link</param>
        /// <returns></returns>
        private UriType CheckVerifyLinkType(
            Uri absoluteUri,UriType linkType)
        {
            if(linkType == UriType.Resource)
            {
                return linkType;
            }
            else
            {
                if (DoIsProcessableUri(absoluteUri, linkType))
                {
                    string head = ResourceDownloader.DownloadHead(absoluteUri, _options);

                    if (string.IsNullOrEmpty(head))
                    {
                        return UriType.Resource;
                    }
                    else
                    {
                        head = head.ToLowerInvariant();
                        if(head.Contains(@"pdf")|| head.Contains(@"application")|| head.Contains(@"image"))
                        {
                            return UriType.Resource;
                        }
                        else
                        {
                            Trace.Assert(head.Contains(@"text"), @"not text document type but marked as content");

                            //The original
                            return linkType;
                        }
                    }
                }
                else
                {
                    return UriType.Resource;
                }
            }
        }



        /// <summary>
        /// Makes an absolute uri from a base Uri and a suplied relative uri
        /// </summary>
        /// <param name="baseUri">The base uri</param>
        /// <param name="uri">the relative uri</param>
        /// <returns></returns>
        private static Uri MakeAbsoluteUri(Uri baseUri,Uri uri)
        {
            if(Uri.IsWellFormedUriString(uri.OriginalString,UriKind.Absolute))
            {
                return uri;
            }
            else
            {
                if(baseUri == null)
                {
                    return uri;
                }
                else
                {
                    string[] pieces = uri.OriginalString.Split('?');

                    UriBuilder builder = new UriBuilder(baseUri.AbsoluteUri);
                    builder.Path = CombineVirtualPath(builder.Path, pieces[0]);

                    if(pieces.Length >1)
                    {
                        builder.Query = pieces[1];
                    }

                    return builder.Uri;
                }
            }
        }



        /// <summary>
        /// Combines two uri path strings together;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private static string CombineVirtualPath(string path, string v)
        {
            if(string.IsNullOrEmpty(path))
            { return v; }

            else if(string.IsNullOrEmpty(v))
            {
                return path;
            }
            else
            {
                path = path.TrimEnd('/');
                v = v.TrimStart('/');

                return path + @"/" + v;
            }
        }


        private static string CleanupUrl(string url)
        {
            //Remove accidentally contained ASP-tags
            url = Regex.Replace(url, @"<%.*?%>", string.Empty, RegexOptions.Singleline);

            //remove anchors
            url = Regex.Replace(url, @"#.*?$", string.Empty, RegexOptions.Singleline);

            return url;
        }


        private static bool DoIsProcessableUri(
            Uri absoluteUri,UriType linkType)
        {
            return
            absoluteUri != null &&
            (DoWantFollowUri(absoluteUri,linkType) || DoIsResourceUri(absoluteUri));
        }

        private static bool DoWantFollowUri(Uri absoluteUri,UriType linkType)
        {
            if(absoluteUri == null || linkType == UriType.Resource)
            {
                return false;
            }
            else
            {
                if (absoluteUri.Scheme == Uri.UriSchemeHttp || absoluteUri.Scheme == Uri.UriSchemeHttps || absoluteUri.Scheme == Uri.UriSchemeFile)
                {
                    return true;
                }
                else
                    return false;
            }
        }
     
        private static bool DoIsResourceUri(Uri absoluteUri)
        {
            return absoluteUri != null &&
                (
                absoluteUri.Scheme == Uri.UriSchemeHttp ||
                absoluteUri.Scheme == Uri.UriSchemeHttps ||
                absoluteUri.Scheme == Uri.UriSchemeFile);
        }
        #endregion
    }

   
}
