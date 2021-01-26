#if UNITY_EDITOR
using System.Collections.Generic;
using OregoFramework.Module;
using OregoFramework.Util;
using UnityEditor;
using UnityEngine;

#pragma warning disable 618

namespace OregoFramework.Edit
{
    [CustomEditor(typeof(LocalizationSpreadsheet))]
    public sealed class LocalizationEditor : Editor
    {
        private LocalizationSpreadsheet spreadsheet;

        private void Awake()
        {
            this.spreadsheet = this.target.As<LocalizationSpreadsheet>();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Reset Spreadsheet"))
            {
                this.ResetSpreadsheet();
            }

            if (GUILayout.Button("Update Spreadsheet"))
            {
                this.UpdateSpreadsheet();
            }

            GUILayout.Space(pixels: 20);
            base.OnInspectorGUI();
        }

        private void ResetSpreadsheet()
        {
            foreach (var page in this.spreadsheet)
            {
                page.dictionaries = new List<LocalizationPage.LanguageDictionary>();
            }
        }

        private void UpdateSpreadsheet()
        {
            Debug.LogError("Editor Coroutine is absent (Add it in Package manager)");
            // StartCoroutine(LocalizationUpdater.UpdateSpreadsheet(this.spreadsheet), this);
        }
    }
}
#endif