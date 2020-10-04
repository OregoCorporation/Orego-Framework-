using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.App
{
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
            IUIScreenTransition transition = null
        )
        {
            var previousScreen = this.currentScreen;
            if (previousScreen != null)
            {
                var previousScreenType = previousScreen.GetType();
                this.previousScreenStack.Push(previousScreenType);
            }

            base.ChangeScreen(sender, screenType, transition);
        }

        public void ChangeToPreviousScreen(object sender, IUIScreenTransition transition = null)
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