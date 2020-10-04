using System;
using System.Collections.Generic;
using OregoFramework.Unit;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class UIScreenController : UIElement, UISystem.IController
    {
        #region Event

        public event Action<UIScreen> OnScreenChangedEvent;

        #endregion

        public UIScreen currentScreen { get; private set; }

        public virtual Transform rootTransform
        {
            get { return this.transform; }
        }

        protected readonly Dictionary<Type, string> screenPathMap;

        protected UIScreenController()
        {
            this.screenPathMap = new Dictionary<Type, string>();
        }

        [SerializeField]
        protected UIScreenConfig config;

        #region Initialize

        public void Initialize()
        {
            this.LoadScreenPaths();
            var defaultScreenType = this.GetDefaultScreenType();
            this.OnInitialize();
            this.StartScreen(this, defaultScreenType);
        }

        private void LoadScreenPaths()
        {
            var infoArray = this.config.array;
            foreach (var screenInfo in infoArray)
            {
                var screenType = Type.GetType(screenInfo.className);
                if (screenType is null)
                {
                    throw new Exception($"Screen type {screenInfo.className} is not found!");
                }

                this.screenPathMap[screenType] = screenInfo.path;
            }
        }

        protected abstract Type GetDefaultScreenType();

        protected virtual void OnInitialize()
        {
        }

        #endregion

        #region Transitions

        public virtual void StartScreen(
            object sender,
            Type screenType,
            IUIScreenTransition transition = null
        )
        {
            var previousScreen = this.currentScreen;
            if (previousScreen != null)
            {
                this.currentScreen = null;
                this.UnloadScreen(previousScreen);
            }

            var nextScreen = this.LoadScreen(screenType);
            nextScreen.OnInitialize(sender, transition);
            this.currentScreen = nextScreen;
            this.OnScreenChangedEvent?.Invoke(this.currentScreen);
        }

        protected virtual UIScreen LoadScreen(Type screenType)
        {
            var path = this.screenPathMap.Find(screenType);
            var prefab = Resources.Load<UIScreen>(path);
            var nextScreen = Instantiate(prefab, this.rootTransform);
            return nextScreen;
        }

        protected virtual void UnloadScreen(UIScreen screen)
        {
            Destroy(screen.gameObject);
        }

        #endregion

        void UISystem.IController.OnRegistered()
        {
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        void UISystem.IController.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }
    }
}