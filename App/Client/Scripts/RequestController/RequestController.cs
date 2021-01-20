using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls a single request case.</para>
    ///     <para>Each controller is responsible for one request case.</para>
    /// </summary>
    public interface IRequestController : IElement
    {
    }
    
    /// <summary>
    ///     <para>An abstract request controller.</para>
    /// </summary>
    public abstract class RequestController<T> : Element, IRequestController where T : IClient
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }

        /// <summary>
        ///     <para>A parent client reference.</para>
        /// </summary>
        protected T Client { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.Client = this.Application.ClientLayer.GetClient<T>();
        }
    }
}