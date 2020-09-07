using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A base class of client layer.</para>
    /// </summary>
    [Using]
    public class ClientLayer : ElementLayer<IClient>, IClientLayer
    {
        /// <inheritdoc cref="IClientLayer.GetClient{T}"/>
        public T GetClient<T>() where T : IClient
        {
            return this.GetElement<T>();
        }
        
        /// <inheritdoc cref="IClientLayer.GetClients{T}"/>
        public IEnumerable<T> GetClients<T>() where T : IClient
        {
            return this.GetElements<T>();
        }
    }
}