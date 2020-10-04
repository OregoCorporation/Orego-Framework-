using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class UICachedScreenController : UIScreenController
    {
        private readonly Stack<Type> previousScreensStack;

        protected UICachedScreenController()
        {
            this.previousScreensStack = new Stack<Type>();
        }

        public sealed override void StartScreen(
            object sender,
            Type screenType,
            IUIScreenTransition transition = null
        )
        {
            var previousScreen = this.currentScreen;
            if (previousScreen != null)
            {
                var previousScreenType = previousScreen.GetType();
                this.previousScreensStack.Push(previousScreenType);
            }

            base.StartScreen(sender, screenType, transition);
        }

        public void StartPreviousScreen(object sender, IUIScreenTransition transition = null)
        {
            var nextScreenType = !this.previousScreensStack.IsEmpty()
                ? this.previousScreensStack.Pop()
                : this.GetDefaultScreenType();
            base.StartScreen(sender, nextScreenType, transition);
        }

        public void ClearStack()
        {
            this.previousScreensStack.Clear();
        }
    }
}