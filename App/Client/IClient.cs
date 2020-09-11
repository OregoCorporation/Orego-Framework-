using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A client interface.</para>
    ///     <para>Each client is responsible for one network connection type.</para>
    ///     <para>Keeps dictionary of unique request controllers <see cref="IRequestController"/>.</para>
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
}