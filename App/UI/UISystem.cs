using System;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class UISystem : UIBehaviour, IUISystem
    {
        public static UISystem instance { get; private set; }

        private readonly Dictionary<Type, IUISystemController> uiControllerMap;

        protected UISystem()
        {
            this.uiControllerMap = new Dictionary<Type, IUISystemController>();
        }

        protected virtual void Awake()
        {
            instance = this;
        }

        public void AddUIController(IUISystemController uiController)
        {
            this.uiControllerMap.Add(uiController.GetType(), uiController);
        }

        public void RemoveUIController(IUISystemController uiController)
        {
            this.uiControllerMap.Remove(uiController.GetType());
        }

        public T GetUIController<T>() where T : IUISystemController
        {
            return DictionaryUtils.Find<T, IUISystemController>(this.uiControllerMap);
        }

        public IEnumerable<T> GetUIControllers<T>() where T : IUISystemController
        {
            return this.uiControllerMap.FindAll<T, IUISystemController>();
        }

        public abstract IElementContext ProvideContext();
    }
}