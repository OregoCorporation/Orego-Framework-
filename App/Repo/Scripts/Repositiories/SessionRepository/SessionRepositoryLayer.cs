using System;
using System.Collections;
using Elementary;

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

        /// <summary>
        ///     <para>Check for updates user data when a user session starts.</para>
        /// </summary>
        protected IUpdateDataSystem updateDataSystem { get; set; }

        protected sealed override void OnCreate(
            ElementLayer<IRepository> _,
            IElementContext context
        )
        {
            this.updateDataSystem = this.CreateUpdateDataSystem();
            this.OnCreate(this, context);
        }

        protected abstract IUpdateDataSystem CreateUpdateDataSystem();

        protected virtual void OnCreate(SessionRepositoryLayer _, IElementContext context)
        {
        }

        /// <inheritdoc cref="ISessionRepositoryLayer.BeginSession"/>
        public IEnumerator BeginSession()
        {
            yield return this.updateDataSystem.CheckForUpdates();
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.BeginSession();
            }

            this.isActiveSession = true;
            yield return this.OnBeginSession();
            this.OnSessionBeganEvent?.Invoke();
        }
        
        protected virtual IEnumerator OnBeginSession()
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