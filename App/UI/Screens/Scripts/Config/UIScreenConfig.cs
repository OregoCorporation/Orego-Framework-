using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OregoFramework.App
{
    [CreateAssetMenu(
        fileName = "ScreenConfig",
        menuName = "Orego/App/UI/New UI Screen Config"
    )]
    public sealed class UIScreenConfig : ScriptableObject
    {
        [SerializeField]
        public ScreenInfo[] array;
        
        [Serializable]
        public sealed class ScreenInfo
        {
            [Header("Path to screen prefab from Resources folder")]
            [SerializeField]
            public string path;

            [Header("Full class name with namespace")]
            [SerializeField]
            public string className;
        }
    }
}