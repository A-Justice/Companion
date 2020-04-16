using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    internal sealed class ResourceStorer
    {
        #region PublicMethods

        public ResourceStorer(
           Settings settings)
        {
            _settings = settings;
        }

        /// <summary>
		/// Stores a binary resource to the local file system.
		/// </summary>
		/// <returns>Return the info about the stored data.</returns>
		public DownloadedResourceInfo StoreBinary(
            byte[] binaryContent,
            UriResourceInfo uriInfo)
        {
            DownloadedResourceInfo result =
                new DownloadedResourceInfo(
                uriInfo,
                _settings.Options.DestinationFolderPath);

            try
            {
                if (result.LocalFilePath.Exists)
                {
                    result.LocalFilePath.Delete();
                }

                if (binaryContent != null && binaryContent.Length > 0)
                {
                    result.LocalFilePath.Create();
                    Trace.WriteLine($"Writing binary content to file '{result.LocalFilePath}'.");

                    using (FileStream s = result.LocalFilePath.OpenWrite())
                    {
                        s.Write(binaryContent, 0, binaryContent.Length);
                    }
                }
            }          
            catch (Exception x)
            {
                Trace.WriteLine($"Ignoring exception while storing binary file: '{ x.Message}'.");
            }

            return result;
        }

        /// <summary>
        /// Stores a HTML resource to the local file system.
        /// Does no hyperlink replacement.
        /// </summary>
        /// <returns>Return the info about the stored data.</returns>
        public DownloadedResourceInfo StoreHtml(
            string textContent,
            Encoding encoding,
            UriResourceInfo uriInfo)
        {
            DownloadedResourceInfo result =
                new DownloadedResourceInfo(
                uriInfo,
                _settings.Options.DestinationFolderPath);

            try
            {
                if (result.LocalFilePath.Exists)
                {
                    result.LocalFilePath.Delete();
                }

                if (!result.LocalFilePath.Directory.Exists)
                {
                    result.LocalFilePath.Directory.Create();
                }

                Trace.WriteLine($"Writing text content to file '{result.LocalFilePath}'.");

                using (FileStream s = new FileStream(result.LocalFilePath.FullName,FileMode.Create,FileAccess.Write))
                using (StreamWriter w = new StreamWriter(s, encoding))
                {
                    w.Write(textContent);
                }
            }
            catch (Exception x)
            {
                Trace.WriteLine($"Ignoring IO exception while storing HTML file: '{x.Message}'.");
            }


            return result;
        }


        #endregion


        #region PrivateVariables
        private readonly Settings _settings;
        #endregion
    }
}
