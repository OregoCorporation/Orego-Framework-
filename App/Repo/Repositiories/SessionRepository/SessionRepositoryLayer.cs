using System.Collections;
using Elementary;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <inheritdoc cref="ISessionRepositoryLayer"/>
    public abstract class SessionRepositoryLayer : RepositoryLayer, ISessionRepositoryLayer
    {
        #region Event
        
        /// <inheritdoc cref="ISessionRepositoryLayer.OnBeginSessionEvent"/>
        public AsyncEvent OnBeginSessionEvent { get; }

        /// <inheritdoc cref="ISessionRepositoryLayer.OnEndSessionEvent"/>
        public AsyncEvent OnEndSessionEvent { get; }

        #endregion

        /// <summary>
        ///     <para>A session started or not.</para>
        /// </summary>
        public bool isActiveSession { get; protected set; }

        protected SessionRepositoryLayer()
        {
            this.OnBeginSessionEvent = new AsyncEvent();
            this.OnEndSessionEvent = new AsyncEvent();
        }

        protected sealed override void OnDispose(ElementLayer<IRepository> _)
        {
            this.OnBeginSessionEvent.Dispose();
            this.OnEndSessionEvent.Dispose();
            this.OnDispose(this);
        }

        protected virtual void OnDispose(SessionRepositoryLayer _)
        {
        }

        /// <inheritdoc cref="ISessionRepositoryLayer.BeginSession"/>
        public IEnumerator BeginSession()
        {
            yield return this.OnBeforeBeginSession();
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.OnBeginSession();
            }

            this.isActiveSession = true;
            yield return this.OnAfterBeginSession();
            yield return this.OnBeginSessionEvent?.Invoke();
        }

        protected virtual IEnumerator OnBeforeBeginSession()
        {
            yield break;
        }

        protected virtual IEnumerator OnAfterBeginSession()
        {
            yield break;
        }

        /// <inheritdoc cref="ISessionRepositoryLayer.EndSession"/>
        public IEnumerator EndSession()
        {
            yield return this.OnBeforeEndSession();
            this.isActiveSession = false;
            yield return this.OnEndSessionEvent?.Invoke();
            var repositories = this.GetRepositories<ISessionRepository>();
            foreach (var repository in repositories)
            {
                yield return repository.OnEndSession();
            }

            yield return this.OnAfterEndSession();
        }

        protected virtual IEnumerator OnBeforeEndSession()
        {
            yield break;
        }

        protected virtual IEnumerator OnAfterEndSession()
        {
            yield break;
        }
    }
}