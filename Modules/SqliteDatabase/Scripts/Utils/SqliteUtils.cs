#if SQL

using System;
using System.Collections;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace OregoFramework.Module
{
    /// <summary>
    ///     <para>Utils.</para>
    /// </summary>
    public static class SqliteUtils
    {
        public static void Read(this DbCommand command, Action<DbDataReader> onRead)
        {
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                onRead.Invoke(reader);
            }
        }

        public static async Task ReadAsync(this DbCommand command, Action<DbDataReader> onRead)
        {
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                onRead.Invoke(reader);
            }
        }

        public static IEnumerator ReinstallDatabase(string originPath, string targetPath)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            yield return InstallDatabase(originPath, targetPath);
        }

        public static IEnumerator InstallDatabaseIfAbsent(string originPath, string targetPath)
        {
            if (!File.Exists(targetPath))
            {
                yield return InstallDatabase(originPath, targetPath);
            }
        }

        public static IEnumerator InstallDatabase(string originPath, string targetPath)
        {
            var request = UnityWebRequest.Get(originPath);
            yield return request.SendWebRequest();
            File.WriteAllBytes(targetPath, request.downloadHandler.data);
        }

        public static (string, string) GetOriginAndTargetPaths(string databaseName)
        {
            var dataPath = UnityEngine.Application.dataPath;
            var persistentDataPath = UnityEngine.Application.persistentDataPath;

            string originPath;
            string targetPath;
#if UNITY_EDITOR
            originPath = $"{dataPath}/StreamingAssets/{databaseName}";
            targetPath = $"{dataPath}/StreamingAssets/{databaseName}";
#elif UNITY_ANDROID
            originPath = $"jar:file://{dataPath}!/assets/{databaseName}";
            targetPath = $"{persistentDataPath}/{databaseName}";
#endif
            return (originPath, targetPath);
        }
    }
}

#endif