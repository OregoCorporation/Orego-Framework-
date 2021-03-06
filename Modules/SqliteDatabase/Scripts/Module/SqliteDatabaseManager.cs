#if SQL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using OregoFramework.Module;
using OregoFramework.Util;
using OregoFramework.Utils;
using UnityEngine;

namespace OregoFramework.App
{
    public class SqliteDatabaseManager : UnitySingleton<SqliteDatabaseManager>
    {
        public static SqliteDatabaseConfig Config
        {
            get { return _instance.config; }
        }

        [SerializeField]
        protected SqliteDatabaseConfig config;
        
        [SerializeField]
        protected UpdaterEntry[] updaterEntries;

        protected readonly Dictionary<int, SqliteDatabaseUpdater> updaterMap;

        public SqliteDatabaseManager()
        {
            this.updaterMap = new Dictionary<int, SqliteDatabaseUpdater>();
        }

        protected virtual void Awake()
        {
            _instance = this;
            foreach (var entry in this.updaterEntries)
            {
                this.updaterMap.Add(entry.version, entry.updater);
            }
        }

        public static IEnumerator Initialize()
        {
            yield return _instance.InitializeInternal();
        }

        protected virtual IEnumerator InitializeInternal()
        {
            var databaseName = this.config.DatabaseName;
            var versionKey = this.config.VersionPrefsKey;

            var originPath = SqliteUtils.GetOriginPath(databaseName);
            var targetPath = SqliteUtils.GetTargetPath(databaseName);

            if (!PlayerPrefs.HasKey(versionKey))
            {
                yield return SqliteUtils.ReinstallDatabase(originPath, targetPath);
                PlayerPrefs.SetInt(versionKey, this.config.Version);
                yield break;
            }

            yield return SqliteUtils.InstallDatabaseIfAbsent(originPath, targetPath);
            yield return this.CheckForUpdates();
        }

        protected IEnumerator CheckForUpdates()
        {
            var targetVersion = this.config.Version;
            
            var versionKey = this.config.VersionPrefsKey;
            var currentVersion = PlayerPrefs.GetInt(versionKey);
            if (currentVersion == targetVersion)
            {
                yield break;
            }

            //Open connection:
            var connectionUri = SqliteUtils.GetConnectionUri(this.config.DatabaseName);
            var connection = new SqliteConnection(connectionUri);
            connection.Open();
            if (connection.State != ConnectionState.Open)
            {
                throw new Exception($"Can't connect to db {connectionUri}!");
            }
            
            //Update database version: 
            for (var version = currentVersion + Int.ONE; version <= targetVersion; version++)
            {
                var hasError = new Reference<bool>();
                yield return this.UpdateDatabase(version, connection, hasError);
                if (hasError.value)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception($"Can't update db to {version} version!");
                }

                PlayerPrefs.SetInt(versionKey, targetVersion);
            }
            
            //Close connection:
            connection.Close();
            connection.Dispose();
        }

        protected IEnumerator UpdateDatabase(int targetVersion, SqliteConnection connection, Reference<bool> hasError)
        {
            if (!this.updaterMap.TryGetValue(targetVersion, out var updater))
            {
                hasError.value = true;
                yield break;
            }

            yield return updater.UpdateDatabase(connection, hasError);
        }
        
        [Serializable]
        protected struct UpdaterEntry
        {
            [SerializeField]
            public int version;

            [SerializeField]
            public SqliteDatabaseUpdater updater;
        }
    }
}
#endif