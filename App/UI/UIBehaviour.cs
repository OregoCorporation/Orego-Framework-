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
        
        private IApplication _application;

        private IApplication application
        {
            get
            {
                if (this._application == null)
                {
                    this._application = this.context.GetRootElement<IApplication>();
                }

                return this._application;
            }
        }

        private IInteractorLayer _interactorLayer;

        private IInteractorLayer interactorLayer
        {
            get
            {
                if (this._interactorLayer == null)
                {
                    this._interactorLayer = this.application.interactorLayer;
                }

                return this._interactorLayer;
            }
        }
        
        protected T GetApplication<T>() where T : IApplication
        {
            return (T) this.application;
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