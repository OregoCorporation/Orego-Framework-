using System.Collections.Generic;
using Elementary;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIElement is the base class from which every UI script derives.</para>
    /// </summary>
    public abstract class UIElement : MonoBehaviour
    {
        private bool uiSystemProvided;

        private UISystem _uiSystem;

        protected UISystem uiSystem
        {
            get
            {
                if (this.uiSystemProvided)
                {
                    return this._uiSystem;
                }

                this._uiSystem = UISystem.instance;
                this.uiSystemProvided = true;
                return this._uiSystem;
            }
        }
        
        
        /// <summary>
        ///     <para>An element context reference.</para>
        /// </summary>
        private IElementContext _elementContext;
        
        protected IElementContext elementContext
        {
            get
            {
                if (this._elementContext == null)
                {
                    this._elementContext = this.uiSystem.ProvideElementaryContext();
                }

                return this._elementContext;
            }
        }

        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        private IApplicationFrame _applicationFrame;

        protected IApplicationFrame applicationFrame
        {
            get
            {
                if (this._applicationFrame == null)
                {
                    this._applicationFrame = this.elementContext.GetRootElement<IApplicationFrame>();
                }

                return this._applicationFrame;
            }
        }

        /// <summary>
        ///     <para>An interactor layer reference.</para>
        /// </summary>
        private IInteractorLayer _interactorLayer;

        protected IInteractorLayer interactorLayer
        {
            get
            {
                if (this._interactorLayer == null)
                {
                    this._interactorLayer = this.applicationFrame.interactorLayer;
                }

                return this._interactorLayer;
            }
        }

        /// <inheritdoc cref="IInteractorLayer.GetInteractor{T}"/>
        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractor<T>();
        }

        /// <inheritdoc cref="IInteractorLayer.GetInteractors{T}"/>
        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractors<T>();
        }
        
        /// <inheritdoc cref="UISystem.GetUIElement{T}"/>
        protected T GetUIElement<T>() where T : UIElement
        {
            return this.uiSystem.GetUIElement<T>();
        }

        /// <inheritdoc cref="UISystem.GetUIElements{T}"/>
        protected IEnumerable<T> GetUIElements<T>() where T : UIElement
        {
            return this.uiSystem.GetUIElements<T>();
        }
    }
}