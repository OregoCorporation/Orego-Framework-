using System;
using System.Collections;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A root singleton class in UI layer.</para>
    /// </summary>
    public abstract class UISystem : MonoBehaviour, IEnumerable<UIElement>
    {
        /// <summary>
        ///     <para>Singleton reference.</para>
        /// </summary>
        public static UISystem Instance { get; private set; }

        /// <summary>
        ///     <para>Registered UI element references.</para>
        /// </summary>
        private readonly Dictionary<Type, UIElement> RegisteredElements;

        protected UISystem()
        {
            this.RegisteredElements = new Dictionary<Type, UIElement>();
        }

        private void Awake()
        {
            Instance = this;
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
            this.RegisteredElements.Add(type, uiElement);
        }

        /// <summary>
        ///     <para>Removes an ui element reference from dictionary of registered elements.</para>
        /// </summary>
        public void UnregisterUIElement(UIElement controller)
        {
            this.RegisteredElements.Remove(controller.GetType());
        }

        /// <summary>
        ///     <para>Returns a registered ui element.</para>
        /// </summary>
        public T GetUIElement<T>() where T : UIElement
        {
            return this.RegisteredElements.Find<T, UIElement>();
        }

        /// <summary>
        ///     <para>Returns registered ui elements.</para>
        /// </summary>
        public IEnumerable<T> GetUIElements<T>() where T : UIElement
        {
            return this.RegisteredElements.FindAll<T, UIElement>();
        }

        public IEnumerator<UIElement> GetEnumerator()
        {
            var elements = this.RegisteredElements.Values;
            foreach (var element in elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}