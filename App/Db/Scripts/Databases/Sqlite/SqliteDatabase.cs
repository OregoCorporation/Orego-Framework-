#if SQL

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
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

        /// <summary>
        ///     <para>Path to database.</para>
        /// </summary>
        protected string DataPath { get; private set; }

        /// <summary>
        ///     <para>Application platform.</para>
        /// </summary>
        protected RuntimePlatform Platform { get; private set; }

        /// <summary>
        ///     <para>Path to application resources.</para>
        /// </summary>
        protected string PersistentDataPath { get; private set; }

        /// <summary>
        ///     <para>Database file name.</para>
        /// </summary>
        protected abstract string DatabaseName { get; }

        public bool IsInitialized { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.DataPath = UnityEngine.Application.dataPath;
            this.Platform = UnityEngine.Application.platform;
            this.PersistentDataPath = UnityEngine.Application.persistentDataPath;
        }
        
        #region Init

        /// <summary>
        ///     <para>Initializes this database state.</para>
        /// </summary>
        public void Initialize()
        {
            string databasePath;
            if (this.Platform == RuntimePlatform.Android)
            {
                this.InitAsAndroid(out databasePath);
            }
            else
            {
                this.InitAsEditor(out databasePath);
            }

            this.ConnectionUri = $"URI=file:{databasePath}";
            this.IsInitialized = true;
        }

        private void InitAsAndroid(out string databasePath)
        {
            databasePath = $"{this.PersistentDataPath}/{this.DatabaseName}";
            if (!File.Exists(databasePath))
            {
                var load = new WWW($"jar:file://{this.DataPath}!/assets/{this.DatabaseName}");
                while (!load.isDone)
                {
                }

                File.WriteAllBytes(databasePath, load.bytes);
            }
        }

        private void InitAsEditor(out string databasePath)
        {
            databasePath = $"{this.DataPath}/StreamingAssets/{this.DatabaseName}";
        }

        #endregion

        #region Connect

        /// <summary>
        ///     <para>Connects to sqlite database.</para>
        /// </summary>
        public IEnumerator Connect()
        {
            if (!this.IsInitialized)
            {
                throw new Exception("Database is not initialized! " +
                                    "Did you forget to call OregoSqliteDatabase.Initialize()");
            }

            this.Connection = new SqliteConnection(this.ConnectionUri);
            this.Connection.Open();
            if (this.Connection.State != ConnectionState.Open)
            {
                throw new Exception("Can not connect to db!");
            }

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
            this.Connection.Close();
            this.Connection.Dispose();
        }
    }
}

#endif