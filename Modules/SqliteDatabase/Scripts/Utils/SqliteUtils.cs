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
//             
// #if UNITY_EDITOR
//             yield break;
// #elif UNITY_ANDROID
//             
// #endif
        }

        public static IEnumerator InstallDatabaseIfAbsent(string originPath, string targetPath)
        {
            if (!File.Exists(targetPath))
            {
                yield return InstallDatabase(originPath, targetPath);
            }
            
// #if UNITY_EDITOR
//             yield break;
// #elif UNITY_ANDROID
//             
// #endif
        }

        public static IEnumerator InstallDatabase(string originPath, string targetPath)
        {
            
            var request = UnityWebRequest.Get(originPath);
            yield return request.SendWebRequest();
            File.WriteAllBytes(targetPath, request.downloadHandler.data);
//             
// #if UNITY_EDITOR
//             yield break;
// #elif UNITY_ANDROID
// #endif
        }

        public static string GetConnectionUri(string databaseName)
        {
            var dbPath = GetTargetPath(databaseName);
            return $"URI=file:{dbPath}";
        }

        public static string GetTargetPath(string databaseName)
        {
            string targetPath;
#if UNITY_EDITOR
            targetPath = $"{UnityEngine.Application.dataPath}/Editor/{databaseName}";
#elif UNITY_ANDROID
            targetPath = $"{UnityEngine.Application.persistentDataPath}/{databaseName}";
#endif
            return targetPath;
        }

        public static string GetOriginPath(string databaseName)
        {
            string originPath;
#if UNITY_EDITOR
            originPath = $"{UnityEngine.Application.dataPath}/StreamingAssets/{databaseName}";
#elif UNITY_ANDROID
            originPath = $"jar:file://{UnityEngine.Application.dataPath}!/assets/{databaseName}";
#endif
            return originPath;
        }
    }
}

#endif