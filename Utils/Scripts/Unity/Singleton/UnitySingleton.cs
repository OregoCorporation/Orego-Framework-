using UnityEngine;

namespace OregoFramework.Util
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
    }
}