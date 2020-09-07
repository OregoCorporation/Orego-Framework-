using System;

namespace OregoFramework.App
{
    public abstract class UIScreen : UIElement
    {
        protected UIScreenController parent { get; private set; }

        protected virtual void Awake()
        {
            this.parent = this.GetComponentInParent<UIScreenController>();
        }
        
        public virtual void OnInitialize(object sender, IUIScreenTransition transition)
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