using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIScreen is an state of application screen.</para>
    /// </summary>
    public abstract class UIScreen : UIElement, IUIStateable
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
        
        protected IUIStateTransition transition { get; set; }

        /// <summary>
        ///     <para>Called when controller has loaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started this screen.</param>
        /// <param name="transition">Input args.</param>
        void IUIStateable.OnEnter(object sender, IUIStateTransition transition)
        {
            this.transition = transition;
            this.OnEnter(sender);
        }

        protected virtual void OnEnter(object sender)
        {
        }

        /// <summary>
        ///     <para>Called when controller has unloaded this screen.</para>
        /// </summary>
        /// <param name="sender">Who has started next screen.</param>
        void IUIStateable.OnExit(object sender)
        {
            this.OnExit(sender);
        }
        
        protected virtual void OnExit(object sender)
        {
        }

        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected virtual void ChangeScreen<T>(IUIStateTransition transition = null) where T : UIScreen
        {
            this.Parent.ChangeScreen<T>(this, transition);
        }

        /// <inheritdoc cref="UIScreenController.ChangeScreen"/>
        protected virtual void ChangeScreen(Type screenType, IUIStateTransition transition = null)
        {
            this.Parent.ChangeScreen(this, screenType, transition);
        }
    }
}