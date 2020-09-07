using System.Collections.Generic;

namespace OregoFramework.App
{
    public interface IUISystem
    {
        void AddUIController(IUISystemController uiController);

        void RemoveUIController(IUISystemController uiController);

        T GetUIController<T>() where T : IUISystemController;

        IEnumerable<T> GetUIControllers<T>() where T : IUISystemController;
    }
}