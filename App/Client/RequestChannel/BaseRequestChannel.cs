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
        /// <summary>
        ///     <para>Registered response listener references.</para>
        /// </summary>
        private readonly HashSet<IResponseListener> registeredResponseListeners;

        /// <summary>
        ///     <para>Registered reset listener references.</para>
        /// </summary>
        private readonly HashSet<IResetListener> registeredResetListeners;

        protected BaseRequestChannel()
        {
            this.registeredResponseListeners = new HashSet<IResponseListener>();
            this.registeredResetListeners = new HashSet<IResetListener>();
        }

        /// <inheritdoc cref="RequestChannel.OnAfterSendRequest"/>
        protected sealed override IEnumerator OnAfterSendRequest(RequestTask request)
        {
            foreach (var listener in this.registeredResponseListeners)
            {
                yield return listener.OnResponse(this, request);
            }

            yield return this.OnAfterRequest(this, request);
        }

        protected virtual IEnumerator OnAfterRequest(BaseRequestChannel _, RequestTask request)
        {
            yield break;
        }

        /// <inheritdoc cref="IResetRequestChannel.Reset"/>
        public void Reset()
        {
            foreach (var resetListener in this.registeredResetListeners)
            {
                resetListener.OnReset(this);
            }

            this.OnReset(this);
        }

        protected virtual void OnReset(BaseRequestChannel _)
        {
        }

        /// <inheritdoc cref="IResponseRequestChannel.RegisterListener"/>
        public void RegisterListener(IResponseListener listener)
        {
            this.registeredResponseListeners.Add(listener);
        }
        
        /// <inheritdoc cref="IResponseRequestChannel.UnregisterListener"/>
        public void UnregisterListener(IResponseListener listener)
        {
            this.registeredResponseListeners.Remove(listener);
        }

        /// <inheritdoc cref="IResetRequestChannel.RegisterListener"/>
        public void RegisterListener(IResetListener listener)
        {
            this.registeredResetListeners.Add(listener);
        }

        /// <inheritdoc cref="IResetRequestChannel.UnregisterListener"/>
        public void UnregisterListener(IResetListener listener)
        {
            this.registeredResetListeners.Remove(listener);
        }
    }
}