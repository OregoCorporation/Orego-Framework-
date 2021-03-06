#if SQL

using UnityEngine;

namespace OregoFramework.App
{
    [CreateAssetMenu(
        fileName = "SqliteDatabaseConfig",
        menuName = "Orego/SQLite/New SqliteDatabaseConfig"
    )]
    public class SqliteDatabaseConfig : ScriptableObject
    {
        [SerializeField]
        public int Version = 2;

        [SerializeField]
        public string DatabaseName = "tp-database.db";

        [SerializeField]
        public string VersionPrefsKey = "TankPuzzlers/db_version";
    }
}
#endif