using System;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>User interface system.</para>
    ///     <para>A root class in UI layer.</para>
    ///     <para>Keeps UI controllers.</para>
    /// </summary>
    public abstract class UISystem : UIBehaviour, IUISystem
    {
        public static UISystem instance { get; private set; }

        private readonly Dictionary<Type, IUISystemController> controllerMap;

        protected UISystem()
        {
            this.controllerMap = new Dictionary<Type, IUISystemController>();
        }

        protected virtual void Awake()
        {
            instance = this;
        }

        public void AddUIController(IUISystemController uiController)
        {
            this.controllerMap.Add(uiController.GetType(), uiController);
        }

        public void RemoveUIController(IUISystemController uiController)
        {
            this.controllerMap.Remove(uiController.GetType());
        }

        public T GetUIController<T>() where T : IUISystemController
        {
            return this.controllerMap.Find<T, IUISystemController>();
        }

        public IEnumerable<T> GetUIControllers<T>() where T : IUISystemController
        {
            return this.controllerMap.FindAll<T, IUISystemController>();
        }

        public abstract IElementContext ProvideContext();
    }
}