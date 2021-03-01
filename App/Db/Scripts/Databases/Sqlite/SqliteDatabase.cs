#if SQL

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;
using UnityEngine;

#pragma warning disable 618

namespace OregoFramework.App
{
    public abstract class SqliteDatabase : Database<ISqliteDao>
    {
        /// <summary>
        ///     <para>A connection to sqlite database.</para>
        /// </summary>
        public DbConnection Connection { get; private set; }

        /// <summary>
        ///     <para>URI database connection.</para>
        /// </summary>
        protected string ConnectionUri { get; private set; }

        public bool IsConnected { get; private set; }

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

            this.ConnectionUri = connectionUri;
            this.Connection = new SqliteConnection(connectionUri);
            this.Connection.Open();
            if (this.Connection.State != ConnectionState.Open)
            {
                throw new Exception("Can not connect to db!");
            }
            
            this.IsConnected = true;

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
            
            this.Connection.Close();
            this.Connection.Dispose();
            this.IsConnected = false;
        }
    }
}

#endif