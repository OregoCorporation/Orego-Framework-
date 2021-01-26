using System;
using OregoFramework.App;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class LocalizationInteractor : Interactor, LocalizationModule.IController
    {
        #region Event

        public event Action OnInitializedEvent;

        public event LocalizationModule.LanguageChangedHandler OnLanguageChangedEvent;

        #endregion

        public SystemLanguage currentLanguage { get; private set; }

        protected LocalizationRepository repository { get; private set; }

        protected LocalizationContent content { get; private set; }

        #region Lifecycle

        protected override void OnPrepare()
        {
            base.OnPrepare();
            LocalizationModule.Initialize(this);
            this.repository = this.GetRepository<LocalizationRepository>();
            this.content = ContentModule.GetContent<LocalizationContent>();
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.repository.OnStateReadyEvent += this.OnRepositoryStateReady;
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            this.repository.OnStateReadyEvent -= this.OnRepositoryStateReady;
        }
        
        #endregion

        public string GetTranslation(string key)
        {
            var localizationConfig = this.content.config;
            if (!localizationConfig.IsLanguageSupported(this.currentLanguage))
            {
                Debug.LogWarning($"Language {this.currentLanguage} is not supported!");
                return key;
            }

            var localizationSpreadsheet = this.content.spreadsheet;
            if (!localizationSpreadsheet.TryTranslate(key, this.currentLanguage, out var result))
            {
                Debug.LogWarning($"Can not translate {key}");
                return key;
            }

            return result;
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            var localizationConfig = this.content.config;
            if (!localizationConfig.IsLanguageSupported(language))
            {
                throw new Exception($"{language} is not supported!");
            }

            this.repository.UpdateLanguage(language);
            this.UpdateLanguage(language);
        }

        public SystemLanguage GetCurrentLanguage()
        {
            return this.currentLanguage;
        }

        private void UpdateLanguage(SystemLanguage language)
        {
            this.currentLanguage = language;
            this.OnLanguageChangedEvent.Invoke(language);
        }

        #region RepositoryCallbacks

        private void OnRepositoryStateReady(LocalizationState state)
        {
            var language = state.language;
            this.UpdateLanguage(language);
            this.OnInitializedEvent?.Invoke();
        }

        #endregion
    }
}