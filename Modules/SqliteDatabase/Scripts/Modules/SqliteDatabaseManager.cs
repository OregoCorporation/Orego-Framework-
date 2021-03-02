#if SQL

using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using OregoFramework.Module;
using OregoFramework.Util;
using OregoFramework.Utils;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class SqliteDatabaseManager : UnitySingleton<SqliteDatabaseManager>
    {
        [SerializeField]
        protected SqliteDatabaseConfig Config;

        [SerializeField]
        protected SqliteDatabaseUpdater[] Updaters;

        protected readonly Dictionary<int, SqliteDatabaseUpdater> UpdaterMap;

        public SqliteDatabaseManager()
        {
            this.UpdaterMap = new Dictionary<int, SqliteDatabaseUpdater>();
        }

        protected virtual void Awake()
        {
            _instance = this;
            foreach (var updater in this.Updaters)
            {
                this.UpdaterMap.Add(updater.TargetVersion, updater);
            }
        }

        public static IEnumerator Initialize()
        {
            yield return _instance.InitializeInternal();
        }

        protected virtual IEnumerator InitializeInternal()
        {
            var databaseName = this.Config.DatabaseName;
            var (originPath, targetPath) = SqliteUtils.GetOriginAndTargetPaths(databaseName);
            yield return this.InstallOrUpdate(originPath, targetPath);
        }

        protected IEnumerator InstallOrUpdate(string originPath, string targetPath)
        {
            var targetVersion = this.Config.Version;
            var versionKey = this.Config.GetVersionPrefsKey();
            if (!PlayerPrefs.HasKey(versionKey))
            {
                yield return SqliteUtils.ReinstallDatabase(originPath, targetPath);
                PlayerPrefs.SetInt(versionKey, targetVersion);
                yield break;
            }

            var currentVersion = PlayerPrefs.GetInt(versionKey);
            if (currentVersion == targetVersion)
            {
                yield break;
            }

            if (currentVersion > targetVersion)
            {
                throw new Exception($"Database {this.Config.DatabaseName} with version {currentVersion} " +
                                    $"more than current version {targetVersion}");
            }

            yield return SqliteUtils.InstallDatabaseIfAbsent(originPath, targetPath);
            yield return this.UpdateDatabaseVersion(targetPath, currentVersion, targetVersion);
        }

        protected IEnumerator UpdateDatabaseVersion(string targetPath, int currentVersion, int targetVersion)
        {
            var versionKey = this.Config.GetVersionPrefsKey();

            var connectionUri = $"URI=file:{targetPath}";
            var connection = new SqliteConnection(connectionUri);
            for (var version = currentVersion + 1; version <= targetVersion; version++)
            {
                var hasError = new Reference<bool>();
                yield return this.UpdateDatabase(version, connection, hasError);
                if (hasError.value)
                {
                    throw new Exception($"Can't update database to {version} version!");
                }

                PlayerPrefs.SetInt(versionKey, targetVersion);
            }
        }


        protected IEnumerator UpdateDatabase(int targetVersion, SqliteConnection connection, Reference<bool> hasError)
        {
            if (!this.UpdaterMap.TryGetValue(targetVersion, out var updater))
            {
                hasError.value = true;
                yield break;
            }

            yield return updater.CheckForUpdates(connection, hasError);
        }
    }
}
#endif