using System;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public class LocalizationLanguageInfo
    {
        [SerializeField]
        public string title;

        [SerializeField]
        public SystemLanguage language;
    }
}