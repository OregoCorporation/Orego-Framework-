using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An abstract request controller.</para>
    /// </summary>
    public abstract class RequestController<T> : Element, IRequestController where T : IClient
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }
        
        /// <summary>
        ///     <para>A parent client reference.</para>
        /// </summary>
        protected T client { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.client = this.applicationFrame.clientLayer.GetClient<T>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(RequestController<T> _)
        {
        }
    }
}