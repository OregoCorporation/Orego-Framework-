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
            [SerializeField]
            [FormerlySerializedAs("path")]
            public string Path;

            [SerializeField]
            [FormerlySerializedAs("className")]
            public string ClassName;
        }
    }
}