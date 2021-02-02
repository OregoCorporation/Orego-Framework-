using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls UI screens. <see cref="UIScreen"/>.</para>
    /// </summary>
    public abstract class UIScreenController : UIElement
    {
        #region Event

        /// <summary>
        ///     <para>Called when screens change.</para>
        ///     <param name="UIScreen">A new screen.</param>
        /// </summary>
        public event Action<object, UIScreen> OnScreenChangedEvent;

        #endregion

        /// <summary>
        ///     <para>Displayed current screen.</para>
        /// </summary>
        public UIScreen CurrentScreen { get; private set; }

        /// <summary>
        ///     <para>Containter for screens.</para>
        /// </summary>
        public virtual Transform RootTransform
        {
            get { return this.transform; }
        }

        [SerializeField]
        protected ClassAsset[] screenAssets;
        
        /// <summary>
        ///     <para>Keeps screen type vs prefab path.</para>
        /// </summary>
        protected readonly Dictionary<Type, string> ScreenPathMap;

        protected UIScreenController()
        {
            this.ScreenPathMap = new Dictionary<Type, string>();
        }

        #region Initialize

        /// <summary>
        ///     <para>Initializes this controller.</para>
        ///     <para>Opens the first screen.</para>
        /// </summary>
        public void Initialize()
        {
            this.SetupScreensMetadata();
            this.OnInitialize();
            this.ChangeScreen(this, this.GetFirstScreenType());
        }

        private void SetupScreensMetadata()
        {
            foreach (var asset in this.screenAssets)
            {
                var screenType = Type.GetType(asset.ClassName);
                if (screenType is null)
                {
                    throw new Exception($"Screen type {asset.ClassName} is not found!");
                }
                
                this.ScreenPathMap.Add(screenType, asset.AssetPath);
            }
        }

        protected abstract Type GetFirstScreenType();

        protected virtual void OnInitialize()
        {
        }

        #endregion

        #region Transitions

        public void ChangeScreen<T>(object sender, IUITransition transition = null) where T : UIScreen
        {
            this.ChangeScreen(sender, typeof(T), transition);
        }
        
        /// <summary>
        ///     <para>Changes screens.</para>
        ///     <para>Closes a previous screen and opens a new screen.</para>
        /// </summary>
        /// <param name="transition">Put args into transition for a new screen.</param>
        /// <typeparam name="T">Type of a new screen.</typeparam>
        public virtual void ChangeScreen(
            object sender,
            Type screenType,
            IUITransition transition = null
        )
        {
            var previousScreen = this.CurrentScreen;
            if (!ReferenceEquals(previousScreen, null))
            {
                this.CurrentScreen = null;
                ((IUITransitionable) previousScreen).OnUnload(sender);
                this.UnloadScreen(previousScreen);
            }

            var nextScreen = this.LoadScreen(screenType);
            ((IUITransitionable) nextScreen).OnLoad(sender, transition);
            this.CurrentScreen = nextScreen;
            this.OnScreenChangedEvent?.Invoke(sender, this.CurrentScreen);
        }

        protected virtual UIScreen LoadScreen(Type screenType)
        {
            var path = DictionaryHelper.Find(this.ScreenPathMap, screenType);
            var prefab = Resources.Load<UIScreen>(path);
            var nextScreen = Instantiate(prefab, this.RootTransform);
            return nextScreen;
        }

        protected virtual void UnloadScreen(UIScreen screen)
        {
            Destroy(screen.gameObject);
        }

        #endregion
    }
}