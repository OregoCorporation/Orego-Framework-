#if SQL

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using Elementary;
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
        public DbConnection connection { get; private set; }

        /// <summary>
        ///     <para>URI database connection.</para>
        /// </summary>
        protected string connectionUri { get; private set; }

        /// <summary>
        ///     <para>Path to database.</para>
        /// </summary>
        protected string dataPath { get; private set; }

        /// <summary>
        ///     <para>Application platform.</para>
        /// </summary>
        protected RuntimePlatform platform { get; private set; }

        /// <summary>
        ///     <para>Path to application resources.</para>
        /// </summary>
        protected string persistentDataPath { get; private set; }

        /// <summary>
        ///     <para>Database file name.</para>
        /// </summary>
        protected abstract string databaseName { get; }

        public bool isInitialized { get; private set; }

        protected sealed override void OnCreate(ElementLayer<ISqliteDao> _, IElementContext context)
        {
            this.dataPath = UnityEngine.Application.dataPath;
            this.platform = UnityEngine.Application.platform;
            this.persistentDataPath = UnityEngine.Application.persistentDataPath;
            this.OnCreate(this);
        }

        protected virtual void OnCreate(SqliteDatabase _)
        {
        }

        #region Init

        /// <summary>
        ///     <para>Initializes this database state.</para>
        /// </summary>
        public void Initialize()
        {
            string databasePath;
            if (this.platform == RuntimePlatform.Android)
            {
                this.InitAsAndroid(out databasePath);
            }
            else
            {
                this.InitAsEditor(out databasePath);
            }

            this.connectionUri = $"URI=file:{databasePath}";
            this.isInitialized = true;
        }

        private void InitAsAndroid(out string databasePath)
        {
            databasePath = $"{this.persistentDataPath}/{this.databaseName}";
            if (!File.Exists(databasePath))
            {
                var load = new WWW($"jar:file://{this.dataPath}!/assets/{this.databaseName}");
                while (!load.isDone)
                {
                }

                File.WriteAllBytes(databasePath, load.bytes);
            }
        }

        private void InitAsEditor(out string databasePath)
        {
            databasePath = $"{this.dataPath}/StreamingAssets/{this.databaseName}";
        }

        #endregion

        #region Connect

        /// <summary>
        ///     <para>Connects to sqlite database.</para>
        /// </summary>
        public IEnumerator Connect()
        {
            if (!this.isInitialized)
            {
                throw new Exception("Database is not initialized! " +
                                    "Did you forget to call OregoSqliteDatabase.Initialize()");
            }

            this.connection = new SqliteConnection(this.connectionUri);
            this.connection.Open();
            if (this.connection.State != ConnectionState.Open)
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
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}

#endif