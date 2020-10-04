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
        ///     <para>Registered UI controller references.</para>
        /// </summary>
        private readonly Dictionary<Type, IController> controllerMap;

        protected UISystem()
        {
            this.controllerMap = new Dictionary<Type, IController>();
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
        ///     <para>Adds an controller reference into dictionary with registered controllers.</para>
        /// </summary>
        public void RegisterController(IController controller)
        {
            this.controllerMap.Add(controller.GetType(), controller);
            controller.OnRegistered();
        }

        /// <summary>
        ///     <para>Removes an controller reference from dictionary with registered controllers.</para>
        /// </summary>
        public void UnregisterController(IController controller)
        {
            this.controllerMap.Remove(controller.GetType());
            controller.OnUnregistered();
        }

        /// <summary>
        ///     <para>Returns a registered controller.</para>
        /// </summary>
        public T GetController<T>() where T : IController
        {
            return this.controllerMap.Find<T, IController>();
        }

        /// <summary>
        ///     <para>Returns registered controllers.</para>
        /// </summary>
        public IEnumerable<T> GetControllers<T>() where T : IController
        {
            return this.controllerMap.FindAll<T, IController>();
        }
        
        /// <summary>
        ///     <para>An UI controller.</para>
        /// </summary>
        public interface IController
        {
            /// <summary>
            ///     <para>Calls when registers to the UI system.</para>
            /// </summary>
            void OnRegistered();

            /// <summary>
            ///     <para>Calls when unregisters from the UI system.</para>
            /// </summary>
            void OnUnregistered();
        }
    }
}