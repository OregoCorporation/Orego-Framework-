using UnityEngine;

namespace OregoFramework.App
{
    public interface IPrefsDao : IDao
    {
    }
    
    /// <summary>
    ///     Player prefs data access object.
    /// </summary>
    public abstract class PrefsDao<T> : Dao<IPrefsDatabase>, IPrefsDao
    {
        /// <summary>
        ///     <para>Database key to player prefs.</para>
        /// </summary>
        protected string Key { get; }

        protected PrefsDao()
        {
            var className = this.GetType().Name;
            this.Key = className;
        }

        /// <summary>
        ///     <para>Returns a saved entity from player prefs</para>
        /// </summary>
        /// 
        /// <param name="key">Saved entity key.</param>
        protected T GetEntity(string key)
        {
            var fullKey = this.GetFullKey(key);
            var stringData = PlayerPrefs.GetString(fullKey);
            return this.DeserializeEntity(stringData);
        }
        
        /// <summary>
        ///     <para>Converts string to "T" entity.</para>
        /// </summary>
        /// 
        /// <param name="stringData">Serialized string.</param>
        protected virtual T DeserializeEntity(string stringData)
        {
            return JsonUtility.FromJson<T>(stringData);
        }

        /// <summary>
        /// <para>Returns contains entity in Player prefs.</para>
        /// </summary>
        /// 
        /// <param name="key">Saved entity key.</param>
        protected bool HasEntity(string key)
        {
            var fullKey = this.GetFullKey(key);
            return PlayerPrefs.HasKey(fullKey);
        }

        /// <summary>
        /// <para>Sets enitiy into Player prefs.</para>
        /// </summary>
        /// 
        /// <param name="key">Saved entity key.</param>
        /// <param name="entity">Saved object</param>
        /// <typeparam name="T">Object type</typeparam>
        protected void SetEntity(string key, T entity)
        {
            var stringData = this.SerializeEntity(entity);
            var fullKey = this.GetFullKey(key);
            PlayerPrefs.SetString(fullKey, stringData);
        }

        /// <summary>
        ///     <para>Converts "T" entity to string.</para>
        /// </summary>
        /// 
        /// <param name="entity">Saved object.</param>
        protected virtual string SerializeEntity(T entity)
        {
            return JsonUtility.ToJson(entity);
        }

        /// <summary>
        ///     <para>Returns a full key to the saved object.</para>
        /// </summary>
        /// 
        /// <param name="key">Saved object key</param>
        protected virtual string GetFullKey(string key)
        {
            return $"{this.Key}/{key}";
        }
    }
}
