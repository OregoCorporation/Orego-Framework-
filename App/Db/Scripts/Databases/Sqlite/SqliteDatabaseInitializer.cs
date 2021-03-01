using System;
using System.Collections;
using System.IO;
using OregoFramework.Util;
using OregoFramework.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace OregoFramework.App
{
    public abstract class SqliteDatabaseInitializer : UnitySingleton<SqliteDatabaseInitializer>
    {
        [SerializeField]
        [Tooltip("Path to Player Prefs")]
        private string versionPath = "YOUR_APP_NAME/db_version";

        [SerializeField]
        private int version = 1;

        /// <summary>
        ///     <para>Application platform.</para>
        /// </summary>
        protected RuntimePlatform Platform { get; private set; }
        
        /// <summary>
        ///     <para>Database file name.</para>
        /// </summary>
        /// 
        protected abstract string DatabaseName { get; }

        /// <summary>
        ///     <para>Path to application resources.</para>
        /// </summary>
        protected string PersistentDataPath { get; private set; }

        /// <summary>
        ///     <para>Path to database.</para>
        /// </summary>
        protected string DataPath { get; private set; }

        
        protected virtual void Awake()
        {
            _instance = this;
            this.DataPath = UnityEngine.Application.dataPath;
            this.Platform = UnityEngine.Application.platform;
            this.PersistentDataPath = UnityEngine.Application.persistentDataPath;
        }
        
        /// <summary>
        ///     <para>Initializes database on device.</para>
        /// </summary>
        public static IEnumerator Initialize(Reference<string> connectionUri)
        {
            yield return _instance.InitializeInternal(connectionUri);
        }

        private IEnumerator InitializeInternal(Reference<string> connectionUri)
        {
            string targetPath;
            if (this.Platform == RuntimePlatform.Android)
            {
                targetPath = $"{this.PersistentDataPath}/{this.DatabaseName}";
                var originPath = $"jar:file://{this.DataPath}!/assets/{this.DatabaseName}";
                yield return this.InitializeDatabase(targetPath, originPath);
            }
            else
            {
                targetPath = $"{this.DataPath}/StreamingAssets/{this.DatabaseName}";
            }

            connectionUri.value = $"URI=file:{targetPath}";
        }

        protected virtual IEnumerator InitializeDatabase(string originPath, string targetPath)
        {
            if (!PlayerPrefs.HasKey(this.versionPath))
            {
                yield return InstallDatabase(originPath, targetPath);
                PlayerPrefs.SetInt(this.versionPath, this.version);
                yield break;
            }

            var currentVersion = PlayerPrefs.GetInt(this.versionPath);
            if (currentVersion == this.version)
            {
                yield break;
            }

            if (currentVersion > this.version)
            {
                throw new Exception($"Database version {currentVersion} more than current version {this.version}");
            }

            if (!File.Exists(targetPath))
            {
                yield return this.InstallDatabase(originPath, targetPath);
            }

            yield return this.UpdateVersion();
        }

        private IEnumerator UpdateVersion()
        {
            var connection = new SqliteConnection(connectionUri)

            for (var i = currentVersion; i < this.version; i++)
            {
                var nextVersion = i + 1;
                yield return this.MigrateTo(nextVersion);
            }
            
            PlayerPrefs.SetInt(this.versionPath, this.version);
        }

        private IEnumerator InstallDatabase(string originPath, string targetPath)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
            
            var request = UnityWebRequest.Get(originPath);
            yield return request.SendWebRequest();
            File.WriteAllBytes(targetPath, request.downloadHandler.data);
        }

        private IEnumerator MigrateTo(int nextVersion)
        {
        }
    }
}