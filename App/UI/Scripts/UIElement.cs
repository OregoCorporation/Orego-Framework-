using System.Collections.Generic;
using Elementary;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An element of UI System <see cref="UISystem"/>.</para>
    /// </summary>
    public abstract class UIElement : MonoBehaviour
    {
        /// <summary>
        ///     <para>Is <see cref="_system"/> is null or not.</para>
        /// </summary>
        private bool isUIAttached;

        /// <summary>
        ///     <para>An UI system reference.</para>
        /// </summary>
        private UISystem _system;

        protected UISystem UI
        {
            get
            {
                if (this.isUIAttached)
                {
                    return this._system;
                }

                this._system = UISystem.Instance;
                this.isUIAttached = true;
                return this._system;
            }
        }
        
        
        /// <summary>
        ///     <para>An element context reference.</para>
        /// </summary>
        private IElementContext _elementatyContext;
        
        protected IElementContext ElementaryContext
        {
            get
            {
                if (this._elementatyContext is null)
                {
                    this._elementatyContext = this.UI.ProvideElementaryContext();
                }

                return this._elementatyContext;
            }
        }

        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        private IApplicationFrame _application;

        protected IApplicationFrame Application
        {
            get
            {
                if (this._application is null)
                {
                    this._application = this.ElementaryContext.GetRootElement<IApplicationFrame>();
                }

                return this._application;
            }
        }

        /// <summary>
        ///     <para>An interactor layer reference.</para>
        /// </summary>
        private IInteractorLayer _interactorLayer;

        protected IInteractorLayer InteractorLayer
        {
            get
            {
                if (this._interactorLayer is null)
                {
                    this._interactorLayer = this.Application.InteractorLayer;
                }

                return this._interactorLayer;
            }
        }

        /// <inheritdoc cref="IInteractorLayer.GetInteractor{T}"/>
        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.InteractorLayer.GetInteractor<T>();
        }

        /// <inheritdoc cref="IInteractorLayer.GetInteractors{T}"/>
        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.InteractorLayer.GetInteractors<T>();
        }
        
        /// <inheritdoc cref="UISystem.GetUIElement{T}"/>
        protected T GetUIElement<T>() where T : UIElement
        {
            return this.UI.GetUIElement<T>();
        }

        /// <inheritdoc cref="UISystem.GetUIElements{T}"/>
        protected IEnumerable<T> GetUIElements<T>() where T : UIElement
        {
            return this.UI.GetUIElements<T>();
        }
    }
}