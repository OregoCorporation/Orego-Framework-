using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A screen controller with previous screen stack.</para>
    /// </summary>
    public abstract class UICachedScreenController : UIScreenController
    {
        private readonly Stack<Type> previousScreenStack;

        protected UICachedScreenController()
        {
            this.previousScreenStack = new Stack<Type>();
        }

        /// <summary>
        ///     <para>Opens a new screen and closes this screen.</para>
        /// </summary>
        /// <param name="transition">Put args into transition for new screen.</param>
        /// <typeparam name="T">Type of a new screen.</typeparam>
        public sealed override void ChangeScreen(
            object sender,
            Type screenType,
            IUITransition transition = null
        )
        {
            var previousScreen = this.CurrentScreen;
            if (!ReferenceEquals(previousScreen, null))
            {
                var previousScreenType = previousScreen.GetType();
                this.previousScreenStack.Push(previousScreenType);
            }

            base.ChangeScreen(sender, screenType, transition);
        }

        public void ChangeToPreviousScreen(object sender, IUITransition transition = null)
        {
            var nextScreenType = !this.previousScreenStack.IsEmpty()
                ? this.previousScreenStack.Pop()
                : this.GetFirstScreenType();
            base.ChangeScreen(sender, nextScreenType, transition);
        }

        public void ClearStack()
        {
            this.previousScreenStack.Clear();
        }
    }
}