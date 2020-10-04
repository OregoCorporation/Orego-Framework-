#if SQL

using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Utils.</para>
    /// </summary>
    public static class SqliteUtils
    {
        public static void ReadSync(this DbCommand command, Action<DbDataReader> onRead)
        {
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                onRead.Invoke(reader);
            }
        }

        public static async Task ReadAsync(
            this DbCommand command,
            Action<DbDataReader> onRead
        )
        {
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                onRead.Invoke(reader);
            }
        }
    }
}

#endif