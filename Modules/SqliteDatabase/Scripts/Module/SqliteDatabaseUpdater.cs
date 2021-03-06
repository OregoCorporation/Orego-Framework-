#if SQL

using System;
using System.Collections;
using System.Data.Common;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class SqliteDatabaseUpdater : MonoBehaviour
    {
        public abstract IEnumerator UpdateDatabase(SqliteConnection connection, Reference<bool> hasError);
        
        /// <summary>
        ///     <para>Execute a query synchronously.</para>
        /// </summary>
        /// 
        /// <param name="commandText">Query request.</param>
        /// <param name="func">Query function.</param>
        protected void Execute(SqliteConnection connection, string commandText, Action<DbCommand> func)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                func.Invoke(command);
            }
        }

        /// <summary>
        ///     <para>Execute a query asynchronously.</para>
        /// </summary>
        /// 
        /// <param name="commandText">Query request.</param>
        /// <param name="asyncFunc">Query function.</param>
        protected IEnumerator Execute(SqliteConnection connection, string commandText, Func<DbCommand, Task> asyncFunc)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            yield return Continuation.Suspend(continuation =>
            {
                Task
                    .Run(() => asyncFunc.Invoke(command))
                    .ContinueWith(it => continuation.Continue())
                    .ContinueWith(it => command.Dispose());
            });
        }
    }
}
#endif