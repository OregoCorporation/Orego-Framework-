using System.Collections;
using Mono.Data.Sqlite;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public class SqliteDatabaseGroupUpdater : SqliteDatabaseUpdater
    {
        [SerializeField]
        private SqliteDatabaseUpdater[] handlers;

        public override IEnumerator UpdateDatabase(SqliteConnection connection, Reference<bool> hasError)
        {
            foreach (var handler in this.handlers)
            {
                yield return handler.UpdateDatabase(connection, hasError);
            }
        }
    }
}