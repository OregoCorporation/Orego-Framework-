using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Database interface.</para>
    /// </summary>
    public interface IDatabase : IElement
    {
        /// <summary>
        ///     <para>Returns a required data access object of "T" type.</para>
        /// </summary>
        T GetDao<T>() where T : IDao;

        /// <summary>
        ///     <para>Returns a required data access objects of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetDaoSet<T>() where T : IDao;
    }
}