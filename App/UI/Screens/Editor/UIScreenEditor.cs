using OregoFramework.App;
using UnityEditor;

#if UNITY_EDITOR
namespace OregoFramework.Edit
{
    internal sealed class UIScreenEditor
    {
        /// <summary>
        ///     <para>Selects the screen config. <see cref="UIScreenConfig"/></para>
        /// </summary>
        [MenuItem("Window/Orego/Config/Show UI Screen Config...")]
        private static void SelectScreenConfigAsset()
        {
            const string path =
                "Assets/Orego/App/UI/Screens/Resources/UIScreenConfig.asset";
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path);
        }
    }
}
#endif