using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Abstract client class.</para>
    /// </summary>
    /// <typeparam name="T">Requred caller type.</typeparam>
    public abstract class Client<T> : ElementLayer<T>, IClient where T : IRequestController
    {
        /// <inheritdoc cref="IClient.GetController{T}"/>
        public TController GetController<TController>() where TController : IRequestController
        {
            return this.GetElement<TController>();
        }

        /// <inheritdoc cref="IClient.GetControllers{T}"/>
        public IEnumerable<TController> GetControllers<TController>() where TController : IRequestController
        {
            return this.GetElements<TController>();
        }
    }
}