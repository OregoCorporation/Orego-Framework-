using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Keeps a metadata of screens.</para>
    /// </summary>
    [CreateAssetMenu(
        fileName = "UIScreenConfig",
        menuName = "Orego/App/UI/New UI Screen Config"
    )]
    public sealed class UIScreenConfig : ScriptableObject
    {
        /// <summary>
        ///     <para>Array of screen metadata</para>
        /// </summary>
        [FormerlySerializedAs("array")]
        [SerializeField]
        public ScreenInfo[] Array;
        
        /// <summary>
        ///     <para>A screen metadata.</para>
        /// </summary>
        [Serializable]
        public sealed class ScreenInfo
        {
            [FormerlySerializedAs("path")]
            [Header("Path to screen prefab from Resources folder")]
            [SerializeField]
            public string Path;

            [FormerlySerializedAs("className")]
            [Header("Full class name with namespace")]
            [SerializeField]
            public string ClassName;
        }
    }
}