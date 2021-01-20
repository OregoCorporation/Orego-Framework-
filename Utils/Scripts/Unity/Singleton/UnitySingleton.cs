using UnityEngine;

namespace Orego.Utils
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
    }
}