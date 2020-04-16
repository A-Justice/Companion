using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    class ResourceWriter
    {
        #region PublicMethods

        /// <summary>
		/// Constructor.
		/// </summary>
		public ResourceWriter(Settings settings)
        {
            _settings = settings;
        }


        /// <summary>
		/// Replace URIs inside a given HTML document that was previously 
		/// downloaded with the local URIs.
		/// </summary>
		/// <returns>Returns the content text with the replaced links.</returns>
		public string ReplaceLinks(
            string textContent,
            UriResourceInfo uriInfo)
        {
            ResourceParser parser = new ResourceParser(
                _settings,
                uriInfo,
                textContent);

            List<UriResourceInfo> linkInfos = parser.ExtractLinks();

            // For remembering duplicates.
            Dictionary<string, string> replacedLinks =
                new Dictionary<string, string>();

            // --

            foreach (UriResourceInfo linkInfo in linkInfos)
            {
                if (linkInfo.WantFollowUri || linkInfo.IsResourceUri)
                {
                    DownloadedResourceInfo dlInfo =
                        new DownloadedResourceInfo(
                        linkInfo,
                        _settings.Options.DestinationFolderPath);

                    //					/*
                    if (!string.IsNullOrEmpty(linkInfo.OriginalUrl))
                    {
                        string textContentBefore = textContent;

                        string link =
                            Regex.Escape(linkInfo.OriginalUrl);

                        textContent = Regex.Replace(
                            textContent,
                            string.Format(@"""{0}""", link),
                            string.Format(@"""{0}""", dlInfo.LocalFileName),
                            RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        textContent = Regex.Replace(
                            textContent,
                            string.Format(@"'{0}'", link),
                            string.Format(@"'{0}'", dlInfo.LocalFileName),
                            RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        // For style-"url(...)"-links.
                        textContent = Regex.Replace(
                            textContent,
                            string.Format(@"\(\s*{0}\s*\)", link),
                            string.Format(@"({0})", dlInfo.LocalFileName),
                            RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        // Some checking.
                        // 2016-10-16, Uwe Keim.
                        if (linkInfo.OriginalUrl != dlInfo.LocalFileName.Name &&
                            textContentBefore == textContent &&
                            !replacedLinks.ContainsKey(linkInfo.AbsoluteUri.AbsolutePath))
                        {
                            throw new ApplicationException($"Failed to replace URI '{linkInfo.OriginalUrl}' with URI '{dlInfo.LocalFileName}' in HTML text '{textContent}'.");
                        }
                        else
                        {
                            // Remember.
                            replacedLinks[linkInfo.AbsoluteUri.AbsolutePath] =
                                linkInfo.AbsoluteUri.AbsolutePath;
                        }
                    }
                    //					*/
                }
            }

            // --

            return textContent;
        }

        #endregion


        #region privateVariables
        private readonly Settings _settings;
        #endregion
    }
}
