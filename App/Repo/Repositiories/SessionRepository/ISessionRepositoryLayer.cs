using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
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
        /// </summary>
        IEnumerator BeginSession();

        /// <summary>
        ///     <para>Finihses a user session.</para>
        /// </summary>
        IEnumerator EndSession();
    }
}