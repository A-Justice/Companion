using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    class WebsiteDownloader : IDisposable
    {

        #region PrivateVariables
        private const int _maxDepth = 500;
        private readonly Settings _settings = new Settings();
        #endregion


        #region PublicMethods

        /// <summary>
		/// Initializes a new instance of the <see cref="WebSiteDownloader"/> 
		/// class.
		/// </summary>
		/// <param name="options">The options.</param>
		public WebsiteDownloader(
            Options options)
        {
            Trace.WriteLine($"Constructing WebSiteDownloader for URI '{ options.DownloadUri}', destination folder path '{ options.DestinationFolderPath}'.");

            _settings = Settings.Restore(options.DestinationFolderPath);
            _settings.Options = options;
        }

        /// <summary>
        /// Performs the complete downloading (synchronously). 
        /// Does return only when completely finished or when an exception
        /// occured.
        /// </summary>
        public void Process()
        {
            string baseUrl =
                _settings.Options.DownloadUri.OriginalString.TrimEnd('/').
                    Split('?')[0];

            if (_settings.Options.DownloadUri.AbsolutePath.IndexOf('/') >= 0 &&
                _settings.Options.DownloadUri.AbsolutePath.Length > 1)
            {
                baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf('/'));
            }

            // --

            // The URI that is configured to be the start URI.
            Uri baseUri = new Uri(baseUrl, UriKind.Absolute);

            // The initial seed.
            DownloadedResourceInfo seedInfo = new DownloadedResourceInfo(
                    _settings.Options,
                    @"/",
                    _settings.Options.DownloadUri,
                    baseUri,
                    _settings.Options.DestinationFolderPath,
                    _settings.Options.DestinationFolderPath,
                    UriType.Content);

            // --

            // Add the first one as the seed.
            if (!_settings.HasContinueDownloadedResourceInfos)
            {
                _settings.AddContinueDownloadedResourceInfos(seedInfo);
            }

            // 20016-10-16, Justice Awuley Addico
            // Doing a multiple looping, to avoid stack overflows.
            // Since a download-"tree" (i.e. the hierachy of all downloadable
            // pages) can get _very_ deep, process one part at a time only.
            // The state is already persisted, so we need to set up again at
            // the previous position.

            int index = 0;
            while (_settings.HasContinueDownloadedResourceInfos)
            {
                // Fetch one.
                DownloadedResourceInfo processInfo =
                    _settings.PopContinueDownloadedResourceInfos();

                Trace.WriteLine($"{index + 1}. loop: Starting processing URLs from '{processInfo.AbsoluteUri.AbsoluteUri}'.");

                // Process the URI, add any continue URIs to start
                // again, later.
                ProcessUrl(processInfo, 0);
                index++;
            }

            Trace.WriteLine($"{index + 1}. loop: Finished processing URLs from seed '{_settings.Options.DownloadUri}'.");
        }

        /// <summary>
        /// Performs the complete downloading (asynchronously). 
        /// Return immediately. Calls the ProcessCompleted event
        /// upon completion.
        /// </summary>
        public void ProcessAsync()
        {
            processAsyncBackgroundWorker = new BackgroundWorker();

            processAsyncBackgroundWorker.WorkerSupportsCancellation = true;

            processAsyncBackgroundWorker.DoWork += processAsyncBackgroundWorker_DoWork;

            processAsyncBackgroundWorker.RunWorkerCompleted += processAsyncBackgroundWorker_RunWorkerCompleted;

            // Start.
            processAsyncBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Cancels a currently running asynchron processing.
        /// </summary>
        public void CancelProcessAsync()
        {
            if (processAsyncBackgroundWorker != null)
            {
                processAsyncBackgroundWorker.CancelAsync();
            }
        }

        #endregion


        #region AsynchronousProcessing

        private BackgroundWorker processAsyncBackgroundWorker = null;

        void processAsyncBackgroundWorker_DoWork( object sender,DoWorkEventArgs e)
        {
            try
            {
                Process();
            }
            catch (StopProcessingException)
            {
                // Do nothing, just end.
            }
        }

        void processAsyncBackgroundWorker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            if (ProcessCompleted != null)
            {
                ProcessCompleted(this, e);
            }
        }

        public delegate void ProcessCompletedEventHandler(object sender,RunWorkerCompletedEventArgs e);

        /// <summary>
        /// Called when the asynchron processing is completed.
        /// </summary>
        public event ProcessCompletedEventHandler ProcessCompleted;

        /// <summary>
        /// 
        /// </summary>
       public class StopProcessingException : Exception
        {
            public  StopProcessingException()
            {
               // this.Message = "The Process has be stoped Finitely";
            }
        }

        #endregion

        #region events

        public class ProcessingUrlEventArgs :
            EventArgs
        {
            #region Private variables.

            private readonly DownloadedResourceInfo uriInfo;
            private readonly int depth;

            #endregion

            #region Public methods.

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ProcessingUrlEventArgs(DownloadedResourceInfo uriInfo,int depth)
            {
                this.uriInfo = uriInfo;
                this.depth = depth;
            }

            #endregion

            #region Public properties.

            /// <summary>
            /// 
            /// </summary>
            public int Depth
            {
                get
                {
                    return depth;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public DownloadedResourceInfo UriInfo
            {
                get
                {
                    return uriInfo;
                }
            }

            #endregion
        }

        public delegate void ProcessingUrlEventHandler(object sender,ProcessingUrlEventArgs e);

        /// <summary>
        /// Called when processing an URL.
        /// </summary>
        public event ProcessingUrlEventHandler ProcessingUrl;

        #endregion


        #region PrivateMethods

        /// <summary>
        /// Process one single URI with a document behind (i.e. no
        /// resource URI).
        /// </summary>
        /// <param name="uriInfo">The URI info.</param>
        /// <param name="depth">The depth.</param>
        private void ProcessUrl(DownloadedResourceInfo uriInfo, int depth)
        {
            Trace.WriteLine($@"Processing URI '{uriInfo.AbsoluteUri.AbsoluteUri}', with depth {depth}.");

            if (_settings.Options.MaximumLinkDepth > 0 && depth > _settings.Options.MaximumLinkDepth)
            {
                Trace.WriteLine($"Depth {uriInfo.AbsoluteUri.AbsoluteUri} exceeds maximum configured depth. Ending recursion " + $"at URI '{depth}'.");
            }
            else if (depth > _maxDepth)
            {
                Trace.WriteLine($"Depth {uriInfo.AbsoluteUri.AbsoluteUri} exceeds maximum allowed recursion depth. " +
                            $"Ending recursion at URI '{depth}' to possible continue later.");

                // Add myself to start there later.
                // But only if not yet process, otherwise we would never finish.
                if (_settings.HasDownloadedUri(uriInfo))
                {
                    Trace.WriteLine($"URI '{uriInfo.AbsoluteUri.AbsoluteUri}' was already downloaded. NOT continuing later.");
                }
                else
                {
                    _settings.AddDownloadedResourceInfo(uriInfo);

                    // Finished the function.

                    Trace.WriteLine($"Added URI '{uriInfo.AbsoluteUri.AbsoluteUri}' to continue later.");
                }
            }
            else
            {
                // If we are in asynchron mode, periodically check for stopps.

                if (processAsyncBackgroundWorker != null)
                {
                    if (processAsyncBackgroundWorker.CancellationPending)
                    {
                        throw new StopProcessingException();
                    }
                }

                // --

                // Notify event sinks about this URL.
                if (ProcessingUrl != null)
                {
                    ProcessingUrlEventArgs e = new ProcessingUrlEventArgs(
                        uriInfo,
                        depth);

                    ProcessingUrl(this, e);
                }

                // --

                if (uriInfo.IsProcessableUri)
                {
                    if (_settings.HasDownloadedUri(uriInfo))
                    {
                        Trace.WriteLine($"URI '{uriInfo.AbsoluteUri.AbsoluteUri}' was already downloaded. Skipping.");
                    }
                    else
                    {
                        Trace.WriteLine($"URI '{uriInfo.AbsoluteUri.AbsoluteUri}' was not already downloaded. Processing.");

                        if (uriInfo.LinkType == UriType.Resource)
                        {
                            Trace.WriteLine($"Processing resource URI '{uriInfo.AbsoluteUri.AbsoluteUri}', with depth {depth}.");

                            byte[] binaryContent;

                            ResourceDownloader.DownloadBinary(
                                uriInfo.AbsoluteUri,
                                out binaryContent,
                                _settings.Options);

                            ResourceStorer storer =
                                new ResourceStorer(_settings);

                            storer.StoreBinary(
                                binaryContent,
                                uriInfo);

                            _settings.AddDownloadedResourceInfo(uriInfo);
                            _settings.PersistDownloadedResourceInfo(uriInfo);
                        }
                        else
                        {
                            Trace.WriteLine($"Processing content URI '{uriInfo.AbsoluteUri.AbsoluteUri}', with depth {depth}.");

                            string textContent;
                            string encodingName;
                            Encoding encoding;
                            byte[] binaryContent;

                            ResourceDownloader.DownloadHtml(
                                uriInfo.AbsoluteUri,
                                out textContent,
                                out encodingName,
                                out encoding,
                                out binaryContent,
                                _settings.Options);

                            ResourceParser parser = new ResourceParser(
                                _settings,
                                uriInfo,
                                textContent);

                            List<UriResourceInfo> linkInfos =
                                parser.ExtractLinks();

                            ResourceWriter rewriter =
                                new ResourceWriter(_settings);
                            textContent = rewriter.ReplaceLinks(
                                textContent,
                                uriInfo);

                            ResourceStorer storer =
                                new ResourceStorer(_settings);

                            storer.StoreHtml(
                                textContent,
                                encoding,
                                uriInfo);

                            // Add before parsing childs.
                            _settings.AddDownloadedResourceInfo(uriInfo);

                            foreach (UriResourceInfo linkInfo in linkInfos)
                            {
                                DownloadedResourceInfo dlInfo =
                                    new DownloadedResourceInfo(
                                        linkInfo,
                                        uriInfo.LocalFolderPath,
                                        uriInfo.LocalBaseFolderPath);

                                // Recurse.
                                ProcessUrl(dlInfo, depth + 1);

                                // Do not return or break immediately if too deep, 
                                // because this would omit certain pages at this
                                // recursion level.
                            }

                            // Persist after completely parsed childs.
                            _settings.PersistDownloadedResourceInfo(uriInfo);
                        }

                        Trace.WriteLine($"Finished processing URI '{uriInfo.AbsoluteUri.AbsoluteUri}'.");
                    }
                }
                else
                {
                    Trace.WriteLine($"URI '{uriInfo.AbsoluteUri.AbsoluteUri}' is not processable. Skipping.");
                }
            }
        }


        #endregion

        #region IDisposableMembers
        public void Dispose()
        {
            
        }
        #endregion

    }
}
