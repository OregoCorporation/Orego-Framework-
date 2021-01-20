using System.Collections;
using System.Collections.Generic;

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
        private readonly HashSet<IResponseRequestListener> registeredResponseListeners;

        /// <summary>
        ///     <para>Registered reset listener references.</para>
        /// </summary>
        private readonly HashSet<IResetRequestListener> registeredResetListeners;

        protected BaseRequestChannel()
        {
            this.registeredResponseListeners = new HashSet<IResponseRequestListener>();
            this.registeredResetListeners = new HashSet<IResetRequestListener>();
        }

        /// <inheritdoc cref="RequestChannel.OnAfterSendRequest"/>
        protected override IEnumerator OnAfterSendRequest(RequestTask request)
        {
            foreach (var listener in this.registeredResponseListeners)
            {
                yield return listener.OnResponse(this, request);
            }
        }
        
        /// <inheritdoc cref="IResetRequestChannel.Reset"/>
        public virtual void Reset()
        {
            foreach (var resetListener in this.registeredResetListeners)
            {
                resetListener.OnReset(this);
            }
        }

        /// <inheritdoc cref="IResponseRequestChannel.RegisterListener"/>
        public void RegisterListener(IResponseRequestListener listener)
        {
            this.registeredResponseListeners.Add(listener);
        }
        
        /// <inheritdoc cref="IResponseRequestChannel.UnregisterListener"/>
        public void UnregisterListener(IResponseRequestListener listener)
        {
            this.registeredResponseListeners.Remove(listener);
        }

        /// <inheritdoc cref="IResetRequestChannel.RegisterListener"/>
        public void RegisterListener(IResetRequestListener listener)
        {
            this.registeredResetListeners.Add(listener);
        }

        /// <inheritdoc cref="IResetRequestChannel.UnregisterListener"/>
        public void UnregisterListener(IResetRequestListener listener)
        {
            this.registeredResetListeners.Remove(listener);
        }
    }
}