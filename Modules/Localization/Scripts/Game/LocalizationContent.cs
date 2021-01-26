using System.Collections;
using Elementary;
using OregoFramework.Module;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class LocalizationContent : Content
    {
        #region Const

        private const string CONFIG_PATH = "LocalizationConfig";

        private const string SPREAD_SHEET_PATH = "LocalizationSpreadsheet";

        #endregion

        public LocalizationConfig config { get; private set; }
        
        public LocalizationSpreadsheet spreadsheet { get; private set; }

        public override IEnumerator PreloadResources()
        {
            yield return this.LoadConfig();
            yield return this.LoadSpreadsheet();
        }

        private IEnumerator LoadSpreadsheet()
        {
            var request = Resources.LoadAsync<LocalizationSpreadsheet>(SPREAD_SHEET_PATH);
            yield return request;
            this.spreadsheet = request.asset.As<LocalizationSpreadsheet>();
        }

        private IEnumerator LoadConfig()
        {
            var request = Resources.LoadAsync<LocalizationConfig>(CONFIG_PATH);
            yield return request;
            this.config = request.asset.As<LocalizationConfig>();
        }
    }
}