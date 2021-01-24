using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;
using UnityEngine.Serialization;

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
        public event Action<UIScreen> OnScreenChangedEvent;

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

        /// <summary>
        ///     <para>Keeps screen type vs prefab path.</para>
        /// </summary>
        protected readonly Dictionary<Type, string> ScreenPathMap;
        
        /// <inheritdoc cref="UIScreenConfig"/> 
        [FormerlySerializedAs("config")]
        [SerializeField]
        protected UIScreenConfig Config;
        
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
            this.LoadScreenPaths();
            this.OnInitialize();
            this.ChangeScreen(this, this.GetFirstScreenType());
        }

        private void LoadScreenPaths()
        {
            var infoArray = this.Config.Array;
            foreach (var screenInfo in infoArray)
            {
                var screenType = Type.GetType(screenInfo.ClassName);
                if (screenType is null)
                {
                    throw new Exception($"Screen type {screenInfo.ClassName} is not found!");
                }
                
                this.ScreenPathMap[screenType] = screenInfo.Path;
            }
        }

        protected abstract Type GetFirstScreenType();

        protected virtual void OnInitialize()
        {
        }

        #endregion

        #region Transitions

        /// <summary>
        ///     <para>Changes screens.</para>
        ///     <para>Closes a previous screen and opens a new screen.</para>
        /// </summary>
        /// <param name="transition">Put args into transition for a new screen.</param>
        /// <typeparam name="T">Type of a new screen.</typeparam>
        public virtual void ChangeScreen(
            object sender,
            Type screenType,
            UIScreenTransition transition = null
        )
        {
            var previousScreen = this.CurrentScreen;
            if (!ReferenceEquals(previousScreen, null))
            {
                this.CurrentScreen = null;
                previousScreen.OnUnload(sender);
                this.UnloadScreen(previousScreen);
            }

            var nextScreen = this.LoadScreen(screenType);
            nextScreen.OnLoaded(sender, transition);
            this.CurrentScreen = nextScreen;
            this.OnScreenChangedEvent?.Invoke(this.CurrentScreen);
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