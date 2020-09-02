using System;
using System.Collections.Generic;
using OregoFramework.Unit;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class UIScreenController : UIElement, IUISystemController
    {
        #region Event

        public event Action<UIScreen> OnScreenChangedEvent;

        #endregion

        protected readonly Dictionary<Type, IResource> screenTypeVsResourceMap;

        protected UIScreenController()
        {
            this.screenTypeVsResourceMap = new Dictionary<Type, IResource>();
        }

        public UIScreen currentScreen { get; private set; }

        public virtual Transform rootTransform
        {
            get { return this.transform; }
        }

        #region Initialize

        public void Initialize()
        {
            this.LoadScreenResources();
            var defaultScreenType = this.GetDefaultScreenType();
            this.OnInitialize();
            this.StartScreen(this, defaultScreenType);
        }

        private void LoadScreenResources()
        {
            var resources = this.ProvideResources();
            foreach (var resource in resources)
            {
                var screenType = resource.screenType;
                this.screenTypeVsResourceMap[screenType] = resource;
            }
        }

        protected virtual IEnumerable<IUIScreenResource> ProvideResources()
        {
            return Resourcer.GetResources<IUIScreenResource>();
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
            var resource = this.screenTypeVsResourceMap.Find(screenType);
            var prefab = Resources.Load<UIScreen>(resource.path);
            var nextScreen = Instantiate(prefab, this.rootTransform);
            return nextScreen;
        }

        protected virtual void UnloadScreen(UIScreen screen)
        {
            Destroy(screen.gameObject);
        }

        #endregion
    }
}