using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A base client class with request channel.</para>
    /// </summary>
    public abstract class BaseClient<T> : Client<T>, IBaseClient where T : IRequestController
    {
        /// <inheritdoc cref="IRequestChannel"/>
        public IRequestChannel channel { get; private set; }

        #region OnCreate

        protected sealed override void OnCreate(ElementLayer<T> _, IElementContext context)
        {
            this.channel = this.CreateChannel();
            this.OnCreate(this);
        }

        /// <summary>
        ///     <para>Creates an instance of request channel.</para>
        /// </summary>
        protected abstract IRequestChannel CreateChannel();

        protected virtual void OnCreate(BaseClient<T> _)
        {
        }

        #endregion
    }
}