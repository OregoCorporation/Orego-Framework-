#if SQL

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;
using OregoFramework.App;

#pragma warning disable 618

namespace OregoFramework.Module
{
    public abstract class SqliteDatabase : Database<ISqliteDao>
    {
        public DbConnection Connection { get; private set; }

        public bool IsConnected
        {
            get { return this.Connection != null; }
        }
        
        protected string ConnectionUri { get; private set; }

        #region Connect

        /// <summary>
        ///     <para>Connects to sqlite database.</para>
        /// </summary>
        public IEnumerator Connect(string connectionUri)
        {
            if (this.IsConnected)
            {
                throw new Exception("Database is already connected!");
            }

            var dbConnection = new SqliteConnection(connectionUri);
            dbConnection.Open();
            if (dbConnection.State != ConnectionState.Open)
            {
                throw new Exception($"Can't connect to db {connectionUri}!");
            }

            this.Connection = dbConnection;
            this.ConnectionUri = connectionUri;

            yield return this.OnConnect();
            var sqlDaoSet = this.GetElements<ISqliteDao>();
            foreach (var sqlDao in sqlDaoSet)
            {
                yield return sqlDao.OnConnect();
            }
        }

        protected virtual IEnumerator OnConnect()
        {
            yield break;
        }

        #endregion

        /// <summary>
        ///     <para>Interrupts the connection with sqlite database.</para>
        /// </summary>
        public void Disconnect()
        {
            if (!this.IsConnected)
            {
                return;
            }

            var connection = this.Connection;
            connection.Close();
            connection.Dispose();
            this.Connection = null;
            this.ConnectionUri = null;
        }
    }
}

#endif