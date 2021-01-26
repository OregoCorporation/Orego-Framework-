using System;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public sealed class LocalizationFontSize
    {
        [SerializeField]
        public SystemLanguage language;

        [SerializeField]
        public int size;
    }
}