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
        ///     <para>Registered UI element references.</para>
        /// </summary>
        private readonly Dictionary<Type, UIElement> registeredUIElements;

        protected UISystem()
        {
            this.registeredUIElements = new Dictionary<Type, UIElement>();
        }

        private void Awake()
        {
            instance = this;
            this.OnAwake();
        }

        protected virtual void OnAwake()
        {
        }
        
        /// <summary>
        ///     <para>Returns an element context reference.</para>
        /// </summary>
        public abstract IElementContext ProvideElementaryContext();

        /// <summary>
        ///     <para>Adds an ui element reference into dictionary of registered elements.</para>
        /// </summary>
        public void RegisterUIElement(UIElement uiElement)
        {
            var type = uiElement.GetType();
            this.registeredUIElements.Add(type, uiElement);
        }

        /// <summary>
        ///     <para>Removes an ui element reference from dictionary of registered elements.</para>
        /// </summary>
        public void UnregisterUIElement(UIElement controller)
        {
            this.registeredUIElements.Remove(controller.GetType());
        }

        /// <summary>
        ///     <para>Returns a registered ui element.</para>
        /// </summary>
        public T GetUIElement<T>() where T : UIElement
        {
            return this.registeredUIElements.Find<T, UIElement>();
        }

        /// <summary>
        ///     <para>Returns registered ui elements.</para>
        /// </summary>
        public IEnumerable<T> GetUIElements<T>() where T : UIElement
        {
            return this.registeredUIElements.FindAll<T, UIElement>();
        }
    }
}