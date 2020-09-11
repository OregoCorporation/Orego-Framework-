using System.Collections;
using Elementary;

namespace OregoFramework.App
{
    public abstract class VersionRepositoryLayer : SessionRepositoryLayer
    {
        public IVersionRepositoryManager versionManager { get; private set; }

        protected sealed override void OnCreate(
            ElementLayer<IRepository> _,
            IElementContext context
        )
        {
            this.versionManager = this.ProvideVersionRepositoryManager();
            this.OnCreate(this);
        }

        protected abstract IVersionRepositoryManager ProvideVersionRepositoryManager();

        protected virtual void OnCreate(VersionRepositoryLayer _)
        {
        }

        protected sealed override IEnumerator OnAfterBeginSession()
        {
            yield return base.OnAfterBeginSession();
            yield return this.versionManager.UpdateVersionInRepositories();
            yield return this.OnAfterBeginSession(this);
        }

        protected virtual IEnumerator OnAfterBeginSession(VersionRepositoryLayer _)
        {
            yield break;
        }
    }
}