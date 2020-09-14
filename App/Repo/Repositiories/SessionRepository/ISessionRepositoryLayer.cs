using System;
using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Maintains a user session.</para>
    /// </summary>
    public interface ISessionRepositoryLayer : IRepoElement
    {
        #region Event

        /// <summary>
        ///     <para>Invoke this event when session is started.</para>
        /// </summary>
        event Action OnSessionBeganEvent;

        /// <summary>
        ///     <para>Invoke this event when session is ended.</para>
        /// </summary>
        event Action OnSessionEndedEvent;
        
        #endregion

        /// <summary>
        ///     <para>Is session started or not.</para>
        /// </summary>
        bool isActiveSession { get; }

        /// <summary>
        ///     <para>Starts a user session asynchronously.</para>
        ///     <para>Loads user data into repositories.</para>
        /// </summary>
        IEnumerator BeginSession();

        /// <summary>
        ///     <para>Finihses a user session asynchronously.</para>
        ///     <para>Unloads user data from repositories.</para>
        /// </summary>
        IEnumerator EndSession();
    }
}