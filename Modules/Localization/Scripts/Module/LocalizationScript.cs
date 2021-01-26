using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class LocalizationScript : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            LocalizationModule.OnLanguageChangedEvent += this.OnLanguageChanged;
        }

        protected virtual void Start()
        {
            if (LocalizationModule.isInitialized)
            {
                var language = LocalizationModule.GetCurrentLanguage();
                this.UpdateState(language);
            }
        }

        protected virtual void OnDisable()
        {
            LocalizationModule.OnLanguageChangedEvent -= OnLanguageChanged;
        }

        private void OnLanguageChanged(SystemLanguage language)
        {
            this.UpdateState(language);
        }

        protected abstract void UpdateState(SystemLanguage language);
    }
}