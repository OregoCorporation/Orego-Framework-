using System;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public sealed class LocalizationSprite
    {
        [SerializeField]
        public SystemLanguage language;

        [SerializeField]
        public Sprite sprite;
    }
}