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
        public int Version = 1;

        [SerializeField]
        public string DatabaseName;

        public virtual string GetVersionPrefsKey()
        {
            return $"{this.DatabaseName}/db_version";
        }
    }
}
#endif