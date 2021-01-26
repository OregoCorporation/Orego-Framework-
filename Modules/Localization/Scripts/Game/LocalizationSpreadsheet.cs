using System.Collections;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    [CreateAssetMenu(
        fileName = "LocalizationSpreadsheet",
        menuName = "Localization/Game/New LocalizationSpreadsheet"
    )]
    public class LocalizationSpreadsheet : ScriptableObject,
        ISerializationCallbackReceiver,
        IEnumerable<LocalizationPage>
    {
        [Header("Google Sheets URI")]
        [TextArea]
        [SerializeField]
        public string uri;

        [Header("Table")]
        [SerializeField]
        private LocalizationPage[] pages;

        private Dictionary<string, LocalizationPage> pageMap;

        public LocalizationPage GetPage(string name)
        {
            return this.pageMap[name];
        }

        public bool TryTranslate(string key, SystemLanguage language, out string result)
        {
            result = null;
            var chunks = key.Split('$');
            if (chunks.Length != 2)
            {
                return false;
            }

            var pageName = chunks[Int.ZERO];
            if (!this.pageMap.TryGetValue(pageName, out var page))
            {
                return false;
            }

            var id = chunks[Int.ONE];
            return page.TryTranslate(language, id, out result);
        }

        public IEnumerator<LocalizationPage> GetEnumerator()
        {
            var pages = this.pageMap.Values;
            foreach (var page in pages)
            {
                yield return page;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.pageMap = new Dictionary<string, LocalizationPage>();
            foreach (var page in this.pages)
            {
                var pageId = page.id;
                this.pageMap.Add(pageId, page);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}