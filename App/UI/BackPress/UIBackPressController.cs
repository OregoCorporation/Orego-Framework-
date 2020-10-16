using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls back press actions.</para>
    /// </summary>
    public abstract class UIBackPressController : UIElement
    {
        protected readonly Stack<Action> onBackPressActionStack;

        public UIBackPressController()
        {
            this.onBackPressActionStack = new Stack<Action>();
        }

        public void FocusAction(Action backPressAction)
        {
            this.onBackPressActionStack.Push(backPressAction);
        }

        public void ReleaseAction(Action backPressAction)
        {
            while (this.onBackPressActionStack.Pop() != backPressAction)
            {
            }
        }

        public void BackPress()
        {
            if (this.onBackPressActionStack.IsNotEmpty())
            {
                var currentBackPressAction = this.onBackPressActionStack.Peek();
                currentBackPressAction?.Invoke();
            }
        }
    }
}