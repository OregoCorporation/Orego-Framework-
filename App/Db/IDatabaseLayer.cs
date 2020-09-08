using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A database layer interface.</para>
    ///     <para>Keeps dictionary of unique databases <see cref="IDatabase"/>.</para>
    /// </summary>
    public interface IDatabaseLayer : IElement
    {
        /// <summary>
        ///     <para>Returns a required database of "T" type.</para>
        /// </summary>
        T GetDatabase<T>() where T : IDatabase;
        
        /// <summary>
        ///     <para>Returns required databases of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetDatabases<T>() where T : IDatabase;
    }
}