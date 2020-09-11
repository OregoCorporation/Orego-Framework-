using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Maintains a user session.</para>
    /// </summary>
    public interface ISessionRepositoryLayer : IRepoElement
    {
        #region Event

        AsyncEvent OnBeginSessionEvent { get; }

        AsyncEvent OnEndSessionEvent { get; }

        #endregion

        /// <summary>
        ///     <para>Is session started or not.</para>
        /// </summary>
        bool isActiveSession { get; }

        /// <summary>
        ///     <para>Starts a user session.</para>
        ///     <para>Loads a user data into repositories.</para>
        /// </summary>
        IEnumerator BeginSession();

        /// <summary>
        ///     <para>Finihses a user session.</para>
        ///     <para>Unloads a user data from repositories.</para>
        /// </summary>
        IEnumerator EndSession();
    }
}