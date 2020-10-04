using System.Collections.Generic;
using Elementary;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>The base class from which every UI script derives.</para>
    /// </summary>
    public abstract class UIElement : MonoBehaviour
    {
        /// <summary>
        ///     <para>Is <see cref="_uiSystem"/> is null or not.</para>
        /// </summary>
        private bool uiSystemProvided;

        /// <summary>
        ///     <para>An UI system reference</para>
        /// </summary>
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
        
        /// <inheritdoc cref="UISystem.GetController{T}"/>
        protected T GetController<T>() where T : UISystem.IController
        {
            return this.uiSystem.GetController<T>();
        }

        /// <inheritdoc cref="UISystem.GetControllers{T}"/>
        protected IEnumerable<T> GetControllers<T>() where T : UISystem.IController
        {
            return this.uiSystem.GetControllers<T>();
        }
    }
}