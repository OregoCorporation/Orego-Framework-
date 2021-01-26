using UnityEngine;

namespace OregoFramework.Module
{
    public static class LocalizationModule
    {
        #region Delegates

        public delegate void LanguageChangedHandler(SystemLanguage language);

        #endregion

        #region Event

        public static event LanguageChangedHandler OnLanguageChangedEvent;

        #endregion

        public static bool isInitialized
        {
            get { return currentController != null; }
        }

        private static IController currentController;

        public static void Initialize(IController controller)
        {
            if (currentController != null)
            {
                currentController.OnLanguageChangedEvent -= OnLanguageChanged;
            }

            currentController = controller;
            currentController.OnLanguageChangedEvent += OnLanguageChanged;
        }

        private static void OnLanguageChanged(SystemLanguage language)
        {
            OnLanguageChangedEvent?.Invoke(language);
        }

        public static string GetTranslation(string key)
        {
            return currentController.GetTranslation(key);
        }

        public static void ChangeLanguage(SystemLanguage language)
        {
            currentController.ChangeLanguage(language);
        }

        public static SystemLanguage GetCurrentLanguage()
        {
            return currentController.GetCurrentLanguage();
        }

        public interface IController
        {
            event LanguageChangedHandler OnLanguageChangedEvent;

            string GetTranslation(string key);

            void ChangeLanguage(SystemLanguage language);

            SystemLanguage GetCurrentLanguage();
        }
    }
}