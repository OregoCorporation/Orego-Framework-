using System;
using System.Collections.Generic;
using UnityEngine;

namespace OregoFramework.Module
{
    public class LocalizationSpriteRenderer : LocalizationScript
    {
        [SerializeField]
        private Entity[] entities;

        protected override void UpdateState(SystemLanguage language)
        {
            foreach (var entity in this.entities)
            {
                entity.renderer.sprite = entity.GetSprite(language);
            }
        }
        
        [Serializable]
        private sealed class Entity : ISerializationCallbackReceiver
        {
            [SerializeField]
            public SpriteRenderer renderer;

            [SerializeField]
            private Sprite defaultSprite;

            [SerializeField]
            private LocalizationSprite[] localizationSprites;

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