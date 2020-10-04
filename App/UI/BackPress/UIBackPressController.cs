using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class UIBackPressController : UIElement, UISystem.IController
    {
        protected readonly Stack<Action> onBackPressActionStack;

        public UIBackPressController()
        {
            this.onBackPressActionStack = new Stack<Action>();
        }

        public void TakeBackPressAction(Action backPressAction)
        {
            this.onBackPressActionStack.Push(backPressAction);
        }

        public void ReleaseBackPressAction(Action backPressAction)
        {
            while (this.onBackPressActionStack.Pop() != backPressAction)
            {
            }
        }

        protected virtual void OnBackPressed()
        {
            if (this.onBackPressActionStack.IsNotEmpty())
            {
                var currentBackPressAction = this.onBackPressActionStack.Peek();
                currentBackPressAction?.Invoke();
            }
        }

        void UISystem.IController.OnRegistered()
        {
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        void UISystem.IController.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }
    }
}