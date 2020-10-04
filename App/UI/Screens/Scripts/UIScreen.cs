using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIScreen is an UI state of application screen.</para>
    /// </summary>
    public abstract class UIScreen : UIElement
    {
        /// <summary>
        ///     <para>Is <see cref="_parent"/> is null or not.</para>
        /// </summary>
        private bool parentProvided;


        /// <summary>
        ///     <para>An UI screen controller reference.</para>
        /// </summary>
        private UIScreenController _parent;

        protected UIScreenController parent
        {
            get
            {
                if (this.parentProvided)
                {
                    return _parent;
                }

                this._parent = this.GetComponentInParent<UIScreenController>();
                this.parentProvided = true;
                return this._parent;
            }
        }

        /// <summary>
        ///     <para>Called when controller has loaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started this screen.</param>
        /// <param name="transition">Input args.</param>
        public virtual void OnLoaded(object sender, IUIScreenTransition transition = null)
        {
        }

        /// <summary>
        ///     <para>Called when controller has unloaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started next screen.</param>
        public virtual void OnUnload(object sender)
        {
        }

        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected void ChangeScreen<T>(IUIScreenTransition transition = null) where T : UIScreen
        {
            this.ChangeScreen(typeof(T), transition);
        }
        
        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected void ChangeScreen(Type screenType, IUIScreenTransition transition = null)
        {
            this.parent.ChangeScreen(this, screenType, transition);
        }
    }
}