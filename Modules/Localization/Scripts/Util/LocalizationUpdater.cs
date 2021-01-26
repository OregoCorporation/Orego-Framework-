using System.Collections;
using OregoFramework.Util;
using UnityEngine;
using UnityEngine.Networking;

namespace OregoFramework.Module
{
    public static class LocalizationUpdater
    {
        public static IEnumerator UpdateSpreadsheet(LocalizationSpreadsheet spreadsheet)
        {
            var uri = spreadsheet.uri;
            foreach (var page in spreadsheet)
            {
                var pageId = page.id;
                var result = new Reference<string>();
                yield return LoadPage(pageId, uri, result);
                page.dictionaries = LocalizationPageBuilder.BuildDictionaries(result.value);
            }
        }

        public static IEnumerator LoadPage(string pageId, string sheetUri, Reference<string> result)
        {
            var uri = $"{sheetUri}/gviz/tq?tqx=out:csv&sheet={pageId}";
            using (var request = UnityWebRequest.Get(uri))
            {
                yield return request.SendWebRequest();
                if (request.isNetworkError)
                {
                    Debug.LogError($"Update page {pageId} error: {request.error}");
                    yield break;
                }

                var text = request.downloadHandler.text;
                Debug.Log($"Page {pageId} is loaded! \n Text: \n {text}");
                result.value = text;
            }
        }

        
    }
}