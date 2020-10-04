using System.Collections.Generic;
using Elementary;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>UIBehaviour is the base class from which every UI script derives.</para>
    /// </summary>
    public abstract class UIBehaviour : MonoBehaviour
    {
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
                    this._elementContext = UISystem.instance.ProvideContext();
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
    }
}