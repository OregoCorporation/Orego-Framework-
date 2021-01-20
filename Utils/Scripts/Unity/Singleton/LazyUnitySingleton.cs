using UnityEngine;

namespace Orego.Utils
{
    public abstract class LazyUnitySingleton<T> : UnitySingleton<T> where T : MonoBehaviour
    {
        protected static T instance
        {
            get
            {
                if (!ReferenceEquals(_instance, null))
                {
                    return _instance;
                }

                CreateSingleton(out _instance);
                return _instance;
            }
        }

        private static void CreateSingleton(out T singleton)
        {
            var gameObject = new GameObject
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            DontDestroyOnLoad(gameObject);
            singleton = gameObject.AddComponent<T>();
        }
    }
}