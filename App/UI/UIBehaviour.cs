using System.Collections.Generic;
using Elementary;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     Unity UI script
    /// </summary>
    public abstract class UIBehaviour : MonoBehaviour
    {
        private IElementContext _context;
        
        private IElementContext context
        {
            get
            {
                if (this._context == null)
                {
                    this._context = UISystem.instance.ProvideContext();
                }

                return this._context;
            }
        }
        
        private IApplicationFrame _applicationFrame;

        private IApplicationFrame applicationFrame
        {
            get
            {
                if (this._applicationFrame == null)
                {
                    this._applicationFrame = this.context.GetRootElement<IApplicationFrame>();
                }

                return this._applicationFrame;
            }
        }

        private IInteractorLayer _interactorLayer;

        private IInteractorLayer interactorLayer
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
        
        protected T GetApplication<T>() where T : IApplicationFrame
        {
            return (T) this.applicationFrame;
        }

        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractor<T>();
        }

        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractors<T>();
        }
    }
}