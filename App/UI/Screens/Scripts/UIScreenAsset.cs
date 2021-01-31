using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A screen metadata.</para>
    /// </summary>
    [CreateAssetMenu(
        fileName = "UIScreenAsset",
        menuName = "Orego/App/UI/New UI UIScreenAsset"
    )]
    public class UIScreenAsset : ScriptableObject
    {
        /// <summary>
        ///     <para>Path to prefab.</para>
        /// </summary>
        [SerializeField]
        public string Path;

        /// <summary>
        ///     <para>Full class name with namespace.</para>
        /// </summary>
        [SerializeField]
        public string ClassName;
    }
}