using UnityEngine;

namespace OregoFramework.Utils
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
    }
}