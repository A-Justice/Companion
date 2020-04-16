using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    public class ResourceDownloader
    {

        #region PrivateVaribles

        /// <summary>
		/// Caching already downloaded HEADs.
		/// </summary>
		private static readonly Dictionary<Uri, string> _headPool =
            new Dictionary<Uri, string>();

        /// <summary>
        /// &lt;meta http-equiv="Content-Type" content="text/html; charset=utf-8"&gt;.
        /// </summary>
        private static readonly string _htmlContentEncodingPattern =
            "<meta\\s+http-equiv\\s*=\\s*[\"'\\s]?Content-Type\\b.*?charset\\s*=\\s*([^\"'\\s>]*)";

        #endregion


        #region PublicMethods

        public static string DownloadHead(Uri absoluteUri, Options _options)
        {
            try
            {
                if (_headPool.ContainsKey(absoluteUri))
                {
                    return _headPool[absoluteUri];
                }

                else
                {
                    Trace.WriteLine($"Reading head from Url {absoluteUri}");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(absoluteUri);
                    request.Method = @"HEAD";

                    RequestCachePolicy cp = new RequestCachePolicy(
                        RequestCacheLevel.BypassCache);
                    request.CachePolicy = cp;

                    request.Proxy = _options.ProxyAddress; 

                    using (HttpWebResponse resp =
                        (HttpWebResponse)request.GetResponse())
                    {
                        _headPool[absoluteUri] = resp.ContentType;
                        return resp.ContentType;
                    }
                }
            }
            catch (Exception ex)
            {
                WebException x = (WebException)ex;

                if (x.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp =
                        (HttpWebResponse)x.Response;

                    if (resp.StatusCode == HttpStatusCode.NotFound ||
                        resp.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Trace.WriteLine($"Ignoring web exception: {x.Message}.");

                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }


        public static void DownloadBinary(Uri absoluteUri,out byte[] binaryContent,Options option)
        {
            Trace.WriteLine($"Reading Content from URL {absoluteUri}");

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(absoluteUri);
                request.Proxy = option.ProxyAddress;

                RequestCachePolicy cp = new RequestCachePolicy(RequestCacheLevel.BypassCache);

                request.CachePolicy = cp;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (MemoryStream memstream = new MemoryStream())
                {
                    int blockSize = 16384;
                    byte[] blockBuffer = new byte[blockSize];
                    int read;

                    while((read = stream.Read(blockBuffer,0,blockSize)) > 0)
                    {
                        memstream.Write(blockBuffer, 0, read);
                    }

                    memstream.Seek(0, SeekOrigin.Begin);
                    binaryContent = memstream.GetBuffer();
                }
            }
            catch(Exception ex)
            {
                WebException x = (WebException)ex;
                if (x.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = (HttpWebResponse)x.Response;

                    if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Trace.WriteLine($"Ignoring web exception {x.Message}");
                        binaryContent = null;
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                    throw;
            }
        }
      
        
        public static void DownloadHtml(Uri absoluteUri,out string textContent,out string encodingName,out Encoding encoding,out byte[] binaryContent,Options options)
        {
            DownloadBinary(absoluteUri, out binaryContent, options);
            encodingName = DetectEncodingName(binaryContent);

            Trace.WriteLine($"Detecting encoding {encodingName} for remote Html document from {absoluteUri}");

            if(binaryContent !=null && binaryContent.Length >0)
            {
                encoding = GetEncodingByName(encodingName);
                textContent = encoding.GetString(binaryContent);

            }
            else
            {
                //default

                encoding = Encoding.Default;
                textContent = null;
            }
        }

       
        #endregion


        #region PrivateMethods

        /// <summary>
        /// Helper function for safely converting a response stream encoding
        /// to a supported Encoding class.
        /// </summary>
        /// <param name="encodingName">Name of the encoding.</param>
        /// <returns></returns>
        public static Encoding GetEncodingByName(string encodingName)
        {
            Encoding encoding = Encoding.Default;

            if (encodingName != null && encodingName.Length > 0)
            {
                try
                {
                    encoding = Encoding.GetEncoding(encodingName);

                }
                catch (Exception ex)
                {
                    encoding = Encoding.Default;

                    Trace.WriteLine($"Unsupported encoding : {encodingName} .Returing default encoding : {encoding}");

                    encoding = Encoding.Default;
                }
            }
            return encoding;
        }

        private static string DetectEncodingName(byte[] binaryContent)
        {
            if(binaryContent == null || binaryContent.Length<=0)
            {
                return null;
            }
            else
            {
                //decode with default encoding to detect the encding Name

                string html = Encoding.Default.GetString(binaryContent);

                //find.
                Match match = Regex.Match(html, _htmlContentEncodingPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (match == null || !match.Success || match.Groups.Count < 2)
                {
                    return null;
                }
                else
                {
                    return match.Groups[1].Value;
                }
            }
        }


        #endregion

    }
}
