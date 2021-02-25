namespace OregoFramework.App
{
    public abstract class UIPopup : UIElement, IUIStateable
    {
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
    }
}