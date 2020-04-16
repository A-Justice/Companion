using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    public class DownloadedResourceInfo:UriResourceInfo,IEquatable<DownloadedResourceInfo>
    {
        #region privateVariables

        private readonly DirectoryInfo _localBaseFolderPath;
        private readonly DirectoryInfo _localFolderPath;
        private readonly FileInfo _localFilePath;
        private readonly FileInfo _localFileName;
        private readonly int _addedByProcessID = Process.GetCurrentProcess().Id;
        private readonly DateTime _dateAdded = DateTime.Now;
        private static readonly string[] _allowedExtensions = new string[]{@"jpg", @"jpeg",@"png",@"gif",@"script",@"html",@"css",@"pdf",@"txt",};

        #endregion


        #region IEquatable<DownloadedResourceInformation> members

        /// <summary>
		/// Indicates whether the current object is equal to another object of 
		/// the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the other parameter; 
		/// otherwise, false.
		/// </returns>
        public bool Equals(DownloadedResourceInfo other)
        {
            if (AbsoluteUri == null && other.AbsoluteUri == null)
            {
                return true;
            }
            else if (AbsoluteUri == null || other.AbsoluteUri == null)
            {
                return false;
            }
            else
            {
                int result =
                    string.Compare(
                        AbsoluteUri.AbsoluteUri,
                        other.AbsoluteUri.AbsoluteUri,
                        true);

                return result == 0;
            }
        }
        #endregion

        #region PublicProperties

        /// <summary>
		/// Gets a value indicating whether [file exists].
		/// </summary>
		/// <value><c>true</c> if [file exists]; otherwise, <c>false</c>.</value>
		public bool FileExists
        {
            get
            {
                FileInfo fileName = MakeLocalFileName(
                    AbsoluteUri,
                    BaseUri,
                    LinkType);

                FileInfo filePath = new FileInfo(
                    Path.Combine(
                        _localFolderPath.FullName,
                        fileName.Name));

                return filePath.Exists;
            }
        }

        /// <summary>
        /// Gets the local base folder path.
        /// </summary>
        /// <value>The local base folder path.</value>
        public DirectoryInfo LocalBaseFolderPath
        {
            get
            {
                return _localBaseFolderPath;
            }
        }

        /// <summary>
        /// Gets the local folder path.
        /// </summary>
        /// <value>The local folder path.</value>
        public DirectoryInfo LocalFolderPath
        {
            get
            {
                return _localFolderPath;
            }
        }

        /// <summary>
        /// Gets the local file path.
        /// </summary>
        /// <value>The local file path.</value>
        public FileInfo LocalFilePath
        {
            get
            {
                return _localFilePath;
            }
        }

        /// <summary>
        /// Gets the name of the local file.
        /// </summary>
        /// <value>The name of the local file.</value>
        public FileInfo LocalFileName
        {
            get
            {
                return _localFileName;
            }
        }

        /// <summary>
        /// Gets the added by process ID.
        /// </summary>
        /// <value>The added by process ID.</value>
        public int AddedByProcessID
        {
            get
            {
                return _addedByProcessID;
            }
        }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>The date added.</value>
        public DateTime DateAdded
        {
            get
            {
                return _dateAdded;
            }
        }
        #endregion

        #region publicMethods

        /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="copyFrom">The copy from.</param>
		public DownloadedResourceInfo(
            DownloadedResourceInfo copyFrom)
			:
			base( copyFrom )
		{
            _localBaseFolderPath = copyFrom._localBaseFolderPath;
            _localFolderPath = copyFrom._localFolderPath;
            _localFilePath = copyFrom._localFilePath;
            _localFileName = copyFrom._localFileName;

            _addedByProcessID = copyFrom._addedByProcessID;
            _dateAdded = copyFrom._dateAdded;
        }


        /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="copyFrom">The copy from.</param>
		/// <param name="baseFolderPath">The base folder path.</param>
		public DownloadedResourceInfo(
            UriResourceInfo copyFrom,
            DirectoryInfo baseFolderPath)
			:
			base( copyFrom )
		{
            _localBaseFolderPath = baseFolderPath;

            _localFilePath = new FileInfo(
                Path.Combine(
                    baseFolderPath.FullName,
                    MakeLocalFileName(
                        copyFrom.AbsoluteUri,
                        copyFrom.BaseUri,
                        copyFrom.LinkType).Name));

            _localFileName =
                new FileInfo(_localFilePath.Name);
        }

        /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="copyFrom">The copy from.</param>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="baseFolderPath">The base folder path.</param>
		public DownloadedResourceInfo(
            UriResourceInfo copyFrom,
            DirectoryInfo folderPath,
            DirectoryInfo baseFolderPath)
			:
			base( copyFrom )
		{
            _localFolderPath = folderPath;
            _localBaseFolderPath = baseFolderPath;
        }

        /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <param name="originalUrl">The original URL.</param>
		/// <param name="uri">The URI.</param>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="baseFolderPath">The base folder path.</param>
		/// <param name="linkType">Type of the link.</param>
		public DownloadedResourceInfo(
            Options options,
            string originalUrl,
            Uri uri,
            Uri baseUri,
            DirectoryInfo folderPath,
            DirectoryInfo baseFolderPath,
            UriType linkType)
			:
			base( options, originalUrl, uri, baseUri, linkType )
		{
            _localFolderPath = folderPath;
            _localBaseFolderPath = baseFolderPath;
        }
        #endregion

        #region PrivateMethods
        private static FileInfo MakeLocalFileName(Uri uri,Uri baseUri,UriType linkType)
        {
            Debug.Assert(uri.IsAbsoluteUri, @"URI must be absolute but it is not.");
           
            //Each uri is unique , use this fact.
            string unique = uri.AbsoluteUri.GetHashCode().ToString(@"X8");

            FileInfo result;

            if(linkType == UriType.Content)
            {
                if (string.Compare(uri.AbsoluteUri.TrimEnd('/'),baseUri.AbsoluteUri.TrimEnd('/'),true) == 0)
                {
                    result = new FileInfo(@"index.html");
                }
                else
                {
                    result = new FileInfo(unique +
                        CorrectFileExtension(
                            TryExtractFileExtension(uri)));
                }
            }
            else
            {
                Debug.Assert(linkType == UriType.Resource);

                result =
                    new FileInfo(unique +
                        CorrectFileExtension(
                            TryExtractFileExtension(uri)));
            }

            Trace.WriteLine($"Making local file path '{result.FullName}' for URI '{uri.AbsoluteUri}'.");

            return result;
        }

        /// <summary>
        /// Corrects the file extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        private static string CorrectFileExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
            {
                return extension;
            }
            else
            {
                extension = extension.TrimStart('.').ToLowerInvariant();

                foreach (string allowedExtension in _allowedExtensions)
                {
                    if (allowedExtension == extension)
                    {
                        return @"." + extension;
                    }
                }

                return @".html";
            }

        }


        /// <summary>
		/// Tries the extract file extension.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		private static string TryExtractFileExtension(
            Uri uri)
        {
            string ext = Path.GetExtension(
                uri.AbsoluteUri.Split('?')[0]);

            return ext;
        }
        #endregion
    }
}
