using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class UICachedScreen : UIScreen
    {
        private bool parentProvided;

        private UICachedScreenController _parent;

        protected new UICachedScreenController parent
        {
            get
            {
                if (this.parentProvided)
                {
                    return this._parent;
                }

                this._parent = base.parent.As<UICachedScreenController>();
                this.parentProvided = true;
                return this._parent;
            }
        }

        protected void StartPreviousScreen(IUIScreenTransition transition = null)
        {
            this.parent.StartPreviousScreen(this, transition);
        }
    }
}