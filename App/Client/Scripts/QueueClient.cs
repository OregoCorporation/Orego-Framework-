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
        protected new QueueRequestChannel Channel { get; private set; }
        
        protected sealed override IRequestChannel CreateChannel()
        {
            this.Channel = this.CreateQueueChannel();
            return this.Channel;
        }

        /// <summary>
        ///     <para>Creates an instance of queue request channel.</para>
        /// </summary>
        protected abstract QueueRequestChannel CreateQueueChannel();
    }
}