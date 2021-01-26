using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace OregoFramework.Module
{
    public class LocalizationText : LocalizationScript
    {
        [SerializeField]
        private List<Entity> entities;

        protected override void UpdateState(SystemLanguage language)
        {
            foreach (var entity in this.entities)
            {
                var text = entity.text;
                if (!entity.ignoreTranslation)
                {
                    var translation = LocalizationModule.GetTranslation(entity.translationKey);
                    text.text = translation;
                }

                if (entity.advanced)
                {
                    if (entity.TryGetFontSize(language, out var fontSize))
                    {
                        text.fontSize = fontSize;
                    }
                }
            }
        }

        [Serializable]
        private sealed class Entity : ISerializationCallbackReceiver
        {
            [SerializeField]
            public Text text;

            [HideIf("ignoreTranslation")]
            [SerializeField]
            public string translationKey;

            [SerializeField]
            public bool ignoreTranslation;

            [SerializeField]
            public bool advanced;

            [ShowIf("advanced")]
            [SerializeField]
            private LocalizationFontSize[] localizationFontSizes = { };

            private Dictionary<SystemLanguage, int> fontSizeMap;

            public bool TryGetFontSize(SystemLanguage language, out int fontSize)
            {
                return this.fontSizeMap.TryGetValue(language, out fontSize);
            }

            void ISerializationCallbackReceiver.OnAfterDeserialize()
            {
                this.fontSizeMap = new Dictionary<SystemLanguage, int>();
                foreach (var localizationFontSize in this.localizationFontSizes)
                {
                    this.fontSizeMap.Add(localizationFontSize.language, localizationFontSize.size);
                }
            }

            void ISerializationCallbackReceiver.OnBeforeSerialize()
            {
            }
        }

#if UNITY_EDITOR

        protected virtual void Reset()
        {
            this.entities = new List<Entity>();
            var texts = this.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                var entity = new Entity
                {
                    text = text
                };
                this.entities.Add(entity);
            }
        }

#endif
    }
}