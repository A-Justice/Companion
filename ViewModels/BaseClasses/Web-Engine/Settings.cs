using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    [Serializable]
    class Settings
    {
        #region PublicMethods

        /// <summary>
        /// Persistently stores this object.
        /// </summary>

        private void Persist()
        {
            try
            {

                string filePath = Path.Combine(
                    _options.DestinationFolderPath.FullName,
                    @"WebSiteDownloader.state");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Debug.WriteLine($"About to persist settings to '{filePath}'. " +
                            $"{_temporaryDownloadedResourceInfos.Count} temporary downloaded resources, " +
                                $"{_persistentDownloadedResourceInfos.Count} persistent downloaded resources, " +
                                $"{_continueDownloadedResourceInfos.Count} continue downloaded resources.");

                BinaryFormatter serializer = new BinaryFormatter();

                using (FileStream writer = new FileStream( filePath,FileMode.CreateNew,FileAccess.Write))
                {
                    serializer.Serialize(writer,this);
                }
            }
            catch (IOException x)
            {
                Console.WriteLine($"Ignoring exception while persisting spider settings: '{x.Message}'.");
            }
           
        }

        /// <summary>
        /// Restore a previously stored setting value from the given
        /// folder path.
        /// </summary>
        /// <returns>Returns an empty object if not found.</returns>

        public static Settings Restore(DirectoryInfo folderPath)
        {
            string filePath = Path.Combine(folderPath.FullName,@"WebSiteDownloader.state");

            if (File.Exists(filePath))
            {
                try
                {
                    BinaryFormatter serializer =new BinaryFormatter();

                    using (FileStream reader = new FileStream(filePath,FileMode.Open,FileAccess.Read))
                    {
                        Settings settings = (Settings)serializer.Deserialize(reader);

                        settings.Options = new Options();
                        

                        if (settings._temporaryDownloadedResourceInfos == null)
                        {
                            settings._temporaryDownloadedResourceInfos = new List<DownloadedResourceInfo>();
                        }
                        if (settings._persistentDownloadedResourceInfos == null)
                        {
                            settings._persistentDownloadedResourceInfos = new List<DownloadedResourceInfo>();
                        }
                        if (settings._continueDownloadedResourceInfos == null)
                        {
                            settings._continueDownloadedResourceInfos = new List<DownloadedResourceInfo>();
                        }

                        // Move from persistent storage back to memory.
                        settings._temporaryDownloadedResourceInfos.Clear();
                        settings._temporaryDownloadedResourceInfos.AddRange(
                            settings._persistentDownloadedResourceInfos);

                        Debug.WriteLine($"Successfully restored settings from '{filePath}'. " +
                            $"{settings._temporaryDownloadedResourceInfos.Count} temporary downloaded resources, " +
                                $"{settings._persistentDownloadedResourceInfos.Count} persistent downloaded resources, " +
                                $"{settings._continueDownloadedResourceInfos.Count} continue downloaded resources.");

                        return settings;
                    }
                }
                catch (SerializationException x)
                {
                    Console.WriteLine(
                        string.Format(
                        @"Ignoring exception while deserializing spider settings: '{0}'.",
                        x.Message));

                    Settings settings = new Settings();
                    

                    return settings;
                }
                catch (IOException x)
                {
                    Console.WriteLine(
                        string.Format(
                        @"Ignoring IO exception while loading spider settings: '{0}'.",
                        x.Message));

                  Settings settings = new Settings();
                   // settings.Options.DestinationFolderPath = folderPath;

                    return settings;
                }
                catch (UnauthorizedAccessException x)
                {
                    Console.WriteLine(
                        string.Format(
                        @"Ignoring exception while loading spider settings: '{0}'.",
                        x.Message));

                    Settings settings = new Settings();
                   // settings.Options.DestinationFolderPath = folderPath;

                    return settings;
                }
            }
            else
            {
                Settings settings = new Settings();
               // settings.Options.DestinationFolderPath = folderPath;

                return settings;
            }
        }

        /// <summary>
        /// Check whether a file was already downloaded.
        /// </summary>
        /// <param name="uriInfo">The URI info.</param>
        /// <returns>
        /// 	<c>true</c> if [has downloaded URI] [the specified URI info]; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool HasDownloadedUri(DownloadedResourceInfo uriInfo)
        {
            //Search whether exist in the list.
            int foundPosition = _temporaryDownloadedResourceInfos.IndexOf(uriInfo);

            if (foundPosition < 0)
            {
                return false;
            }
            else
            {
                //found .Check various attributes.
                DownloadedResourceInfo foundInfo = _temporaryDownloadedResourceInfos[foundPosition];

                if(foundInfo.AddedByProcessID == Process.GetCurrentProcess().Id)
                {
                    return true;
                }
                else if(foundInfo.DateAdded.AddHours(10) >DateTime.Now)
                {
                    return true;

                }
                else
                {
                    return foundInfo.FileExists;
                }
          }


        }

        /// <summary>
        /// Add information about a downloaded resource.
        /// </summary>
        /// <param name="info">The info.</param>

        public void AddDownloadedResourceInfo(DownloadedResourceInfo uriInfo)
        {
            if (_temporaryDownloadedResourceInfos.Contains(uriInfo))
            {
                _temporaryDownloadedResourceInfos.Remove(uriInfo);

            }
            _temporaryDownloadedResourceInfos.Add(uriInfo);
        }

        /// <summary>
        /// Persist information about a downloaded resource.
        /// </summary>
        /// <param name="uriInfo">The URI info.</param>

        public void PersistDownloadedResourceInfo(DownloadedResourceInfo uriInfo)
        {
            int foundPosition = _temporaryDownloadedResourceInfos.IndexOf(uriInfo);

            DownloadedResourceInfo foundInfo = _temporaryDownloadedResourceInfos[foundPosition];

            //move
            if(_persistentDownloadedResourceInfos.Contains(foundInfo))
                {

                _persistentDownloadedResourceInfos.Remove(foundInfo);
                 }
            _persistentDownloadedResourceInfos.Add(foundInfo);

            Persist();
        }

        /// <summary>
        /// The URLs where to continue parsing when the stack Console gets too deep.
        /// </summary>
        /// <value>The continue downloaded resource infos.</value>
        public void AddContinueDownloadedResourceInfos(DownloadedResourceInfo resourceInfo)
        {
            if (_continueDownloadedResourceInfos.Contains(resourceInfo))
            {
                _continueDownloadedResourceInfos.Remove(resourceInfo);

            }
            _continueDownloadedResourceInfos.Add(resourceInfo);
            Persist();
        }

        /// <summary>
        /// Pops the continue downloaded resource infos.
        /// </summary>
        /// <returns>Returns the first entry or NULL if none.</returns>
        public DownloadedResourceInfo PopContinueDownloadedResourceInfos()
        {
            if (_continueDownloadedResourceInfos.Count <= 0)
            {
                return null;
            }
            else
            {
                DownloadedResourceInfo result = _continueDownloadedResourceInfos[0];
                _continueDownloadedResourceInfos.RemoveAt(0);
                Persist();
                return result;
            }

        }


        #endregion


        #region publicProperties
        /// <summary>
		/// The options.
		/// </summary>
		/// <value>The options.</value>
		public Options Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has continue
        /// downloaded resource infos.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has continue downloaded resource
        /// infos; otherwise, <c>false</c>.
        /// </value>
        public bool HasContinueDownloadedResourceInfos
        {
            get
            {
                return _continueDownloadedResourceInfos.Count > 0;
            }
        }
        #endregion

        #region PrivateVariables
        [NonSerialized]
        private Options _options =
            new Options();

        [NonSerialized]
        private List<DownloadedResourceInfo> _temporaryDownloadedResourceInfos =
            new List<DownloadedResourceInfo>();

        private List<DownloadedResourceInfo> _persistentDownloadedResourceInfos =
            new List<DownloadedResourceInfo>();

        /// <summary>
        /// The URLs where to continue parsing when the stack Console gets too deep.
        /// </summary>
        private List<DownloadedResourceInfo> _continueDownloadedResourceInfos =
            new List<DownloadedResourceInfo>();
        #endregion
    }
}
