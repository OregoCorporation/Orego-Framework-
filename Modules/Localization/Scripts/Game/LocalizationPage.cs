using System;
using System.Collections.Generic;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public class LocalizationPage : ISerializationCallbackReceiver
    {
        [SerializeField]
        public string id;

        [SerializeField]
        public List<LanguageDictionary> dictionaries;

        private Dictionary<SystemLanguage, Dictionary<string, string>> langaugeTable;

        public bool TryTranslate(SystemLanguage language, string id, out string result)
        {
            result = null;
            if (!this.langaugeTable.TryGetValue(language, out var dictionary))
            {
                return false;
            }

            if (!dictionary.TryGetValue(id, out result))
            {
                return false;
            }

            return true;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.langaugeTable = new Dictionary<SystemLanguage, Dictionary<string, string>>();
            foreach (var dictionary in this.dictionaries)
            {
                var translationMap = new Dictionary<string, string>();
                var entities = dictionary.entities;
                foreach (var entity in entities)
                {
                    translationMap.Add(entity.key, entity.translation);
                }

                var languageText = dictionary.language;
                if (!Enum.TryParse(languageText, out SystemLanguage language))
                {
                    throw new Exception($"Can not parse language text: {languageText}");
                }

                this.langaugeTable.Add(language, translationMap);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        [Serializable]
        public sealed class LanguageDictionary
        {
            [SerializeField]
            public string language;

            [SerializeField]
            public List<LanguageEntity> entities;
        }

        [Serializable]
        public sealed class LanguageEntity
        {
            [SerializeField]
            public string key;

            [TextArea(minLines: 0, maxLines: 5)]
            [SerializeField]
            public string translation;
        }
    }
}