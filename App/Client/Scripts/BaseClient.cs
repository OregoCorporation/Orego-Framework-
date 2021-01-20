namespace OregoFramework.App
{
    public interface IBaseClient : IClient
    {
        /// <summary>
        ///     <para>Sends requests on low level.</para>
        /// </summary>
        IRequestChannel Channel { get; }
    }
    
    /// <summary>
    ///     <para>A base client class with request channel.</para>
    /// </summary>
    public abstract class BaseClient<T> : Client<T>, IBaseClient where T : IRequestController
    {
        /// <inheritdoc cref="IRequestChannel"/>
        public IRequestChannel Channel { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Channel = this.CreateChannel();
        }

        /// <summary>
        ///     <para>Creates an instance of request channel.</para>
        /// </summary>
        protected abstract IRequestChannel CreateChannel();
    }
}