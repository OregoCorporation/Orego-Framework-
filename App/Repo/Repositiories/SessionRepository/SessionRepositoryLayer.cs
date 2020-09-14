using System;
using System.Collections;

namespace OregoFramework.App
{
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
        public bool isActiveSession { get; protected set; }
        
        /// <inheritdoc cref="ISessionRepositoryLayer.BeginSession"/>
        public IEnumerator BeginSession()
        {
            yield return this.OnBeforeBeginSession();
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.BeginSession();
            }

            this.isActiveSession = true;
            yield return this.OnAfterBeginSession();
            this.OnSessionBeganEvent?.Invoke();
        }

        /// <summary>
        ///     <para>Called before repositories are initialized.</para>
        /// </summary>
        protected virtual IEnumerator OnBeforeBeginSession()
        {
            yield break;
        }

        /// <summary>
        ///     <para>Called after repositories are initialized.</para>
        /// </summary>
        protected virtual IEnumerator OnAfterBeginSession()
        {
            yield break;
        }

        /// <inheritdoc cref="ISessionRepositoryLayer.EndSession"/>
        public IEnumerator EndSession()
        {
            yield return this.OnBeforeEndSession();
            this.isActiveSession = false;
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