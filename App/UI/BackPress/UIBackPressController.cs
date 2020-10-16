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
        /// <summary>
        ///     <para>Keeps back press actions.</para>
        /// </summary>
        protected readonly Stack<Action> onBackPressActionStack;

        public UIBackPressController()
        {
            this.onBackPressActionStack = new Stack<Action>();
        }
        
        /// <summary>
        ///     <para>Push back press action into head of stack.</para>
        /// </summary>
        /// <param name="backPressAction">Back press action</param>
        public void FocusAction(Action backPressAction)
        {
            this.onBackPressActionStack.Push(backPressAction);
        }

        /// <summary>
        ///     <para>Pop back press action from head of stack.</para>
        /// </summary>
        /// <param name="backPressAction">Back press action</param>
        public void ReleaseAction(Action backPressAction)
        {
            while (this.onBackPressActionStack.Pop() != backPressAction)
            {
            }
        }

        /// <summary>
        ///     <para>Invokes head back press action in back press stack</para>
        /// </summary>
        public void BackPress()
        {
            if (this.onBackPressActionStack.IsNotEmpty())
            {
                var headBackPressAction = this.onBackPressActionStack.Peek();
                headBackPressAction?.Invoke();
            }
        }
    }
}