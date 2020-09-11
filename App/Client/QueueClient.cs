using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Contains a queue request system.</para>
    /// </summary>
    public abstract class QueueClient<T> : BaseClient<T> where T : BaseRequestController
    {
        /// <summary>
        ///     <para>A request channel with queue.</para>
        /// </summary>
        protected new QueueRequestChannel channel { get; private set; }

        /// <summary>
        ///     <para>Called when request have been sent.</para>
        /// </summary>
        protected IEnumerable<IResponseListener> onResponseListeners { get; private set; }

        /// <summary>
        ///     <para>Called when channel state has been reset.</para>
        /// </summary>
        protected IEnumerable<IResetListener> onResetListeners { get; private set; }

        #region OnCreate

        protected sealed override IRequestChannel CreateChannel()
        {
            this.channel = this.CreateQueueChannel();
            return this.channel;
        }

        /// <summary>
        ///     <para>Creates an instance of queue request channel.</para>
        /// </summary>
        protected abstract QueueRequestChannel CreateQueueChannel();

        protected sealed override void OnCreate(BaseClient<T> _)
        {
            this.onResponseListeners = this.CreateResponseListeners();
            this.onResetListeners = this.CreateResetListeners();
            this.OnCreate(this);
        }

        /// <summary>
        ///     <para>Create response listeners.</para>
        /// </summary>
        protected abstract IEnumerable<IResponseListener> CreateResponseListeners();

        /// <summary>
        ///     <para>Create reset listeners.</para>
        /// </summary>
        protected abstract IEnumerable<IResetListener> CreateResetListeners();

        /// <summary>
        ///     <para>Creates an instance of queue request channel.</para>
        /// </summary>
        protected virtual void OnCreate(QueueClient<T> _)
        {
        }

        #endregion

        #region OnPrepare

        protected sealed override void OnPrepare(Element _)
        {
            this.RegisterChannelListeners();
            this.OnPrepare(this);
        }

        private void RegisterChannelListeners()
        {
            foreach (var listener in this.onResponseListeners)
            {
                this.channel.RegisterListener(listener);
            }

            foreach (var listener in this.onResetListeners)
            {
                this.channel.RegisterListener(listener);
            }
        }

        protected virtual void OnPrepare(QueueClient<T> _)
        {
        }

        #endregion

        #region OnFinish

        protected override void OnFinish(Element _)
        {
            this.UnregisterChannelListeners();
            this.OnFinish(this);
        }

        private void UnregisterChannelListeners()
        {
            foreach (var listener in this.onResponseListeners)
            {
                this.channel.UnregisterListener(listener);
            }

            foreach (var listener in this.onResetListeners)
            {
                this.channel.UnregisterListener(listener);
            }
        }

        private void OnFinish(QueueClient<T> _)
        {
        }

        #endregion
    }
}