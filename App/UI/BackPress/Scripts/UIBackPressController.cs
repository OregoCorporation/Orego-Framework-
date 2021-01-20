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
        protected readonly Stack<Action> BackPressActionStack;

        public UIBackPressController()
        {
            this.BackPressActionStack = new Stack<Action>();
        }
        
        /// <summary>
        ///     <para>Push back press action into head of stack.</para>
        /// </summary>
        /// <param name="backPressAction">Back press action</param>
        public void FocusAction(Action backPressAction)
        {
            this.BackPressActionStack.Push(backPressAction);
        }

        /// <summary>
        ///     <para>Pop back press action from head of stack.</para>
        /// </summary>
        /// <param name="backPressAction">Back press action</param>
        public void ReleaseAction(Action backPressAction)
        {
            while (this.BackPressActionStack.Pop() != backPressAction)
            {
            }
        }

        /// <summary>
        ///     <para>Invokes head back press action in back press stack</para>
        /// </summary>
        public void BackPress()
        {
            if (this.BackPressActionStack.IsNotEmpty())
            {
                var headBackPressAction = this.BackPressActionStack.Peek();
                headBackPressAction?.Invoke();
            }
        }
    }
}