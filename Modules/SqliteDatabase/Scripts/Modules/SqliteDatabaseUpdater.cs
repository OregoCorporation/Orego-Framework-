#if SQL

using System.Collections;
using Mono.Data.Sqlite;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class SqliteDatabaseUpdater : MonoBehaviour
    {
        public int TargetVersion
        {
            get { return this.targetVersion; }
        }

        [SerializeField]
        private int targetVersion;

        public abstract IEnumerator CheckForUpdates(SqliteConnection connection, Reference<bool> hasError);
    }
}
#endif