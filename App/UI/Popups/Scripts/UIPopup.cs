using System;

namespace OregoFramework.App
{
    public abstract class UIPopup : UIElement, IUITransitionable
    {
        #region Event

        public event Action<UIPopup> OnDisposeEvent;

        #endregion

        void IUITransitionable.OnLoad(object sender, IUITransition transition)
        {
            this.OnLoad(sender, transition);
        }

        protected virtual void OnLoad(object sender, IUITransition transition)
        {
        }

        void IUITransitionable.OnUnload(object sender)
        {
            this.OnUnload(sender);
        }

        protected virtual void OnUnload(object sender)
        {
        }

        protected virtual void Dispose()
        {
            this.OnDisposeEvent?.Invoke(this);
        }
    }
}