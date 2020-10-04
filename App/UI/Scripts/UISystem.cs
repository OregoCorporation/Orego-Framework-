using System;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A root singleton class in UI layer.</para>
    /// </summary>
    public abstract class UISystem : MonoBehaviour
    {
        /// <summary>
        ///     <para>Singleton reference.</para>
        /// </summary>
        public static UISystem instance { get; private set; }

        /// <summary>
        ///     <para>Registered UI elements.</para>
        /// </summary>
        private readonly Dictionary<Type, UIElement> registeredUIElementMap;

        protected UISystem()
        {
            this.registeredUIElementMap = new Dictionary<Type, UIElement>();
        }

        private void Awake()
        {
            instance = this;
            this.OnAwake();
        }

        protected virtual void OnAwake()
        {
        }
        
        public abstract IElementContext ProvideElementaryContext();

        public void RegisterUIElement(UIElement uiElement)
        {
            this.registeredUIElementMap.Add(uiElement.GetType(), uiElement);
        }

        public void UnregisterUIElement(UIElement uiElement)
        {
            this.registeredUIElementMap.Remove(uiElement.GetType());
        }

        public T GetUIElement<T>() where T : UIElement
        {
            return this.registeredUIElementMap.Find<T, UIElement>();
        }

        public IEnumerable<T> GetUIElements<T>() where T : UIElement
        {
            return this.registeredUIElementMap.FindAll<T, UIElement>();
        }
    }
}