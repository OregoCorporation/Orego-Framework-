using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIScreen is an state of application screen.</para>
    /// </summary>
    public abstract class UIScreen : UIElement
    {
        /// <summary>
        ///     <para>Is <see cref="_parent"/> is null or not.</para>
        /// </summary>
        private bool isParentProvided;
        
        /// <summary>
        ///     <para>An UI screen controller reference.</para>
        /// </summary>
        private UIScreenController _parent;

        protected UIScreenController Parent
        {
            get
            {
                if (this.isParentProvided)
                {
                    return _parent;
                }

                this._parent = this.GetComponentInParent<UIScreenController>();
                this.isParentProvided = true;
                return this._parent;
            }
        }

        /// <summary>
        ///     <para>Called when controller has loaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started this screen.</param>
        /// <param name="transition">Input args.</param>
        public virtual void OnLoaded(object sender, UIScreenTransition transition = null)
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
        protected void ChangeScreen<T>(UIScreenTransition transition = null) where T : UIScreen
        {
            this.ChangeScreen(typeof(T), transition);
        }
        
        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected void ChangeScreen(Type screenType, UIScreenTransition transition = null)
        {
            this.Parent.ChangeScreen(this, screenType, transition);
        }
    }
}