using System.Collections.Generic;
using UnityEngine;

namespace OregoFramework.Module
{
    [CreateAssetMenu(
        fileName = "LocalizationConfig",
        menuName = "Localization/Game/New LocalizationConfig"
    )]
    public class LocalizationConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        public SystemLanguage defaultLanguage = SystemLanguage.English;

        [SerializeField]
        private LocalizationLanguageInfo[] supportLanguages;

        private Dictionary<SystemLanguage, LocalizationLanguageInfo> languageMap;

        public bool IsLanguageSupported(SystemLanguage language)
        {
            return this.languageMap.ContainsKey(language);
        }

        public LocalizationLanguageInfo GetInfo(SystemLanguage language)
        {
            return this.languageMap[language];
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.languageMap = new Dictionary<SystemLanguage, LocalizationLanguageInfo>();
            foreach (var languageInfo in this.supportLanguages)
            {
                this.languageMap.Add(languageInfo.language, languageInfo);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}