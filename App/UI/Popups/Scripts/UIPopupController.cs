using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public class UIPopupController : UIElement
    {
        #region Event

        public event Action<object, UIPopup> OnShowPopupEvent;

        public event Action<object, UIPopup> OnHidePopupEvent;

        #endregion

        /// <summary>
        ///     <para>Containter for screens.</para>
        /// </summary>
        public virtual Transform RootTransform
        {
            get { return this.transform; }
        }

        [SerializeField]
        protected ClassAsset[] popupAssets;

        protected readonly Dictionary<Type, string> PopupPathMap;

        protected readonly Dictionary<Type, UIPopup> DisplayedPopupMap;

        protected UIPopupController()
        {
            this.PopupPathMap = new Dictionary<Type, string>();
            this.DisplayedPopupMap = new Dictionary<Type, UIPopup>();
        }

        #region Initialize

        /// <summary>
        ///     <para>Initializes this controller.</para>
        ///     <para>Opens the first screen.</para>
        /// </summary>
        public void Initialize()
        {
            this.SetupPopupsMetadata();
            this.OnInitialize();
        }

        private void SetupPopupsMetadata()
        {
            foreach (var asset in this.popupAssets)
            {
                var popupType = Type.GetType(asset.ClassName);
                if (popupType is null)
                {
                    throw new Exception($"Popup type {asset.ClassName} is not found!");
                }

                this.PopupPathMap.Add(popupType, asset.AssetPath);
            }
        }

        protected virtual void OnInitialize()
        {
        }

        #endregion

        public bool IsDisplayedPopup<T>()
        {
            return this.IsDisplayedPopup(typeof(T));
        }

        public bool IsDisplayedPopup(Type popupType)
        {
            return this.DisplayedPopupMap.ContainsKey(popupType);
        }

        public UIPopup ShowPopup<T>(object sender, IUIStateTransition transition = null)
        {
            return this.ShowPopup(sender, typeof(T), transition);
        }

        public virtual UIPopup ShowPopup(object sender, Type popupType, IUIStateTransition transition = null)
        {
            var popup = this.LoadPopup(popupType);
            ((IUIStateable) popup).OnEnter(sender, transition);
            this.DisplayedPopupMap.Add(popupType, popup);
            this.OnShowPopupEvent?.Invoke(sender, popup);
            return popup;
        }

        public void HidePopup(object sender, UIPopup popup)
        {
            var popupType = popup.GetType();
            this.DisplayedPopupMap.Remove(popupType);
            ((IUIStateable) popup).OnExit(sender);
            this.OnHidePopupEvent?.Invoke(sender, popup);
            this.UnloadPopup(popup);
        }

        public void HideAllPopups()
        {
            var displayedPopups = this.GetDisplayedPopups();
            foreach (var popup in displayedPopups)
            {
                this.HidePopup(this, popup);
            }
        }

        public UIPopup GetDisplayedPopup<T>()
        {
            return this.GetDisplayedPopup(typeof(T));
        }

        public UIPopup GetDisplayedPopup(Type popupType)
        {
            return this.DisplayedPopupMap[popupType];
        }

        public IEnumerable<UIPopup> GetDisplayedPopups()
        {
            return new HashSet<UIPopup>(this.DisplayedPopupMap.Values);
        }

        protected virtual UIPopup LoadPopup(Type popupType)
        {
            var path = DictionaryHelper.Find(this.PopupPathMap, popupType);
            var prefab = Resources.Load<UIPopup>(path);
            var popup = Instantiate(prefab, this.RootTransform);
            return popup;
        }

        protected virtual void UnloadPopup(UIPopup popup)
        {
            Destroy(popup.gameObject);
        }
    }
}