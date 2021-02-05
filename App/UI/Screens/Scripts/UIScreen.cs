using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIScreen is an state of application screen.</para>
    /// </summary>
    public abstract class UIScreen : UIElement, IUITransitionable
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
        
        protected IUITransition transition { get; set; }

        /// <summary>
        ///     <para>Called when controller has loaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started this screen.</param>
        /// <param name="transition">Input args.</param>
        void IUITransitionable.OnLoad(object sender, IUITransition transition)
        {
            this.transition = transition;
            this.OnLoad(sender, transition);
        }

        protected virtual void OnLoad(object sender, IUITransition transition)
        {
        }

        /// <summary>
        ///     <para>Called when controller has unloaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started next screen.</param>
        void IUITransitionable.OnUnload(object sender)
        {
            this.OnUnload(sender);
        }
        
        protected virtual void OnUnload(object sender)
        {
        }

        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected virtual void ChangeScreen<T>(IUITransition transition = null) where T : UIScreen
        {
            this.Parent.ChangeScreen<T>(this, transition);
        }

        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected virtual void ChangeScreen(Type screenType, IUITransition transition = null)
        {
            this.Parent.ChangeScreen(this, screenType, transition);
        }
    }
}