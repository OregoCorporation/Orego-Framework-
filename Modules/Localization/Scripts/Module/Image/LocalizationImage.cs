using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OregoFramework.Module
{
    public class LocalizationImage : LocalizationScript
    {
        [SerializeField]
        private List<Entity> entities;

        protected override void UpdateState(SystemLanguage language)
        {
            foreach (var entity in this.entities)
            {
                entity.image.sprite = entity.GetSprite(language);
            }
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            this.entities = new List<Entity>();
            var images = this.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                var entity = new Entity
                {
                    image = image,
                    defaultSprite = image.sprite
                };
                this.entities.Add(entity);
            }
        }
#endif

        [Serializable]
        private sealed class Entity : ISerializationCallbackReceiver
        {
            [SerializeField]
            public Image image;

            [SerializeField]
            public Sprite defaultSprite;

            [SerializeField]
            private LocalizationSprite[] localizationSprites = {};

            private Dictionary<SystemLanguage, Sprite> spriteMap;

            public Sprite GetSprite(SystemLanguage language)
            {
                if (this.spriteMap.TryGetValue(language, out var sprite))
                {
                    return sprite;
                }

                return this.defaultSprite;
            }

            void ISerializationCallbackReceiver.OnAfterDeserialize()
            {
                this.spriteMap = new Dictionary<SystemLanguage, Sprite>();
                foreach (var localizationSprite in this.localizationSprites)
                {
                    this.spriteMap.Add(localizationSprite.language, localizationSprite.sprite);
                }
            }

            void ISerializationCallbackReceiver.OnBeforeSerialize()
            {
            }
        }
    }
}