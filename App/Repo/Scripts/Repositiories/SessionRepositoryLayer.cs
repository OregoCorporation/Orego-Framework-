using System;
using System.Collections;
using Elementary;

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
        bool IsActiveSession { get; }

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
    
    /// <inheritdoc cref="ISessionRepositoryLayer"/>
    public abstract class SessionRepositoryLayer : RepositoryLayer, ISessionRepositoryLayer
    {
        #region Event

        /// <summary>
        ///     <inheritdoc cref="ISessionRepositoryLayer.OnSessionBeganEvent"/>
        /// </summary>
        public event Action OnSessionBeganEvent;

        /// <summary>
        ///     <inheritdoc cref="ISessionRepositoryLayer.OnSessionEndedEvent"/>
        /// </summary>
        public event Action OnSessionEndedEvent;

        #endregion

        /// <summary>
        ///     <para>A session started or not.</para>
        /// </summary>
        public bool IsActiveSession { get; protected set; }

        /// <summary>
        ///     <para>Check for updates user data when a user session starts.</para>
        /// </summary>
        protected IUpdateDataSystem UpdateDataSystem { get; set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.UpdateDataSystem = this.CreateUpdatingDataSystem();
        }

        protected abstract IUpdateDataSystem CreateUpdatingDataSystem();
        
        /// <inheritdoc cref="ISessionRepositoryLayer.BeginSession"/>
        public IEnumerator BeginSession()
        {
            yield return this.UpdateDataSystem.CheckForUpdates();
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.BeginSession();
            }

            this.IsActiveSession = true;
            yield return this.OnSessionBegan();
            this.OnSessionBeganEvent?.Invoke();
        }
        
        protected virtual IEnumerator OnSessionBegan()
        {
            yield break;
        }

        /// <inheritdoc cref="ISessionRepositoryLayer.EndSession"/>
        public IEnumerator EndSession()
        {
            yield return this.OnBeforeEndSession();
            this.IsActiveSession = false;
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.EndSession();
            }

            yield return this.OnAfterEndSession();
            this.OnSessionEndedEvent?.Invoke();
        }

        /// <summary>
        ///     <para>Called before repositories are finished.</para>
        /// </summary>
        protected virtual IEnumerator OnBeforeEndSession()
        {
            yield break;
        }

        /// <summary>
        ///     <para>Called after repositories are finished.</para>
        /// </summary>
        protected virtual IEnumerator OnAfterEndSession()
        {
            yield break;
        }
    }
}