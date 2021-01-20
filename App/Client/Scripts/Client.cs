using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A client interface.</para>
    ///     <para>Each client is responsible for one network connection type.</para>
    ///     <para>Keeps a dictionary of unique request controllers <see cref="IRequestController"/>.</para>
    /// </summary>
    public interface IClient : IElement
    {
        /// <summary>
        ///     <para>Returns a required request controller of "T" type.</para>
        /// </summary>
        T GetController<T>() where T : IRequestController;

        /// <summary>
        ///     <para>Returns required request controllers of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetControllers<T>() where T : IRequestController;
    }
    
    /// <summary>
    ///     <para>Abstract client class.</para>
    /// </summary>
    /// <typeparam name="T">Keeps a dictionary of unique request controllers with "T" type.</typeparam>
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