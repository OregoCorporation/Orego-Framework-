using UnityEngine;

namespace OregoFramework.Util
{
    /// <summary>
    ///     <para>A class metadata.</para>
    /// </summary>
    [CreateAssetMenu(
        fileName = "ClassAsset",
        menuName = "Orego/Util/New ClassAsset"
    )]
    public class ClassAsset : ScriptableObject
    {
        /// <summary>
        ///     <para>Path to asset.</para>
        /// </summary>
        [SerializeField]
        public string AssetPath;

        /// <summary>
        ///     <para>Full class name with namespace.</para>
        /// </summary>
        [SerializeField]
        public string ClassName;
    }
}