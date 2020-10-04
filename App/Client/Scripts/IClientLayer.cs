using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A client layer interface.</para>
    ///     <para>Keeps dictionary of unique clients <see cref="IClient"/>.</para>
    /// </summary>
    public interface IClientLayer : IElement
    {
        /// <summary>
        ///     <para>Returns a required client of "T" type.</para>
        /// </summary>
        T GetClient<T>() where T : IClient;

        /// <summary>
        ///     <para>Returns required clients of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetClients<T>() where T : IClient;
    }
}