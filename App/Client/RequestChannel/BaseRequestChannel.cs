using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A base class request channel with request listeners.</para>
    /// </summary>
    public abstract class BaseRequestChannel : RequestChannel,
        IResponseRequestChannel,
        IResetRequestChannel
    {
        private readonly HashSet<IResponseListener> onResponseListeners;

        private readonly HashSet<IResetListener> onResetListeners;

        protected BaseRequestChannel()
        {
            this.onResponseListeners = new HashSet<IResponseListener>();
            this.onResetListeners = new HashSet<IResetListener>();
        }

        protected sealed override IEnumerator OnAfterRequest(RequestTask request)
        {
            foreach (var listener in this.onResponseListeners)
            {
                yield return listener.OnResponse(this, request);
            }

            yield return this.OnAfterRequest(this, request);
        }

        protected virtual IEnumerator OnAfterRequest(BaseRequestChannel _, RequestTask request)
        {
            yield break;
        }

        public void Reset()
        {
            foreach (var resetListener in this.onResetListeners)
            {
                resetListener.OnReset(this);
            }

            this.OnReset(this);
        }

        protected virtual void OnReset(BaseRequestChannel _)
        {
        }

        public void RegisterListener(IResponseListener listener)
        {
            this.onResponseListeners.Add(listener);
        }

        public void UnregisterListener(IResponseListener listener)
        {
            this.onResponseListeners.Remove(listener);
        }

        public void RegisterListener(IResetListener listener)
        {
            this.onResetListeners.Add(listener);
        }

        public void UnregisterListener(IResetListener listener)
        {
            this.onResetListeners.Remove(listener);
        }
    }
}