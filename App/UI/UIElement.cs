using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     UI elment of system.
    /// </summary>
    public abstract class UIElement : UIBehaviour
    {
        private bool isUiSystemBound;

        private IUISystem _uiSystem;

        private IUISystem uiSystem
        {
            get
            {
                if (!this.isUiSystemBound)
                {
                    this._uiSystem = UISystem.instance;
                    this.isUiSystemBound = true;
                }

                return this._uiSystem;
            }
        }

        protected T GetUISystem<T>() where T : IUISystem
        {
            return (T) this.uiSystem;
        }

        protected T GetUIController<T>() where T : IUISystemController
        {
            return this.uiSystem.GetUIController<T>();
        }

        protected IEnumerable<T> GetUIControllers<T>() where T : IUISystemController
        {
            return this.uiSystem.GetUIControllers<T>();
        }
    }
}