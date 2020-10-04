using System;

namespace OregoFramework.App
{
    public abstract class UIScreen : UIElement
    {
        protected UIScreenController parent
        {
            get
            {
                if (this.parentProvided)
                {
                    return _parent;
                }

                this._parent = this.GetComponentInParent<UIScreenController>();
                this.parentProvided = true;
                return this._parent;
            }
        }

        private bool parentProvided;

        private UIScreenController _parent;

        public virtual void OnInitialize(object sender, IUIScreenTransition transition = null)
        {
        }

        protected void StartScreen<T>(IUIScreenTransition transition = null) where T : UIScreen
        {
            this.StartScreen(typeof(T), transition);
        }

        protected void StartScreen(Type screenType, IUIScreenTransition transition = null)
        {
            this.parent.StartScreen(this, screenType, transition);
        }
    }
}