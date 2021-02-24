using System;

namespace OregoFramework.App
{
    public abstract class UIPopup : UIElement, IUIStateable
    {
        #region Event

        public event Action<UIPopup> OnDisposeEvent;

        #endregion

        protected IUIStateTransition transition { get; set; }

        void IUIStateable.OnEnter(object sender, IUIStateTransition transition)
        {
            this.transition = transition;
            this.OnEnter(sender);
        }

        protected virtual void OnEnter(object sender)
        {
        }

        void IUIStateable.OnExit(object sender)
        {
            this.OnExit(sender);
        }

        protected virtual void OnExit(object sender)
        {
        }

        protected virtual void Dispose()
        {
            this.OnDisposeEvent?.Invoke(this);
        }
    }
}