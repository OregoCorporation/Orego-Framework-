using System.Collections;
using Elementary;

namespace OregoFramework.App
{
    public abstract class UpdateVersionRepositoryLayer : AsyncRepositoryLayer
    {
        public IUpdateVersionRepositoryManager updateVersionManager { get; private set; }

        #region OnCreate

        protected sealed override void OnCreate(
            ElementLayer<IRepository> _,
            IElementContext context
        )
        {
            this.updateVersionManager = this.ProvideUpdateVersionRepositoryManager();
            this.OnPrepare(this);
        }

        protected abstract IUpdateVersionRepositoryManager ProvideUpdateVersionRepositoryManager();

        protected virtual void OnCreate(UpdateVersionRepositoryLayer _)
        {
        }

        #endregion

        #region BeginSession

        protected sealed override IEnumerator OnAfterBeginSession()
        {
            yield return base.OnAfterBeginSession();
            yield return this.updateVersionManager.UpdateVersionInRepositories();
            yield return this.OnAfterBeginSession(this);
        }

        protected virtual IEnumerator OnAfterBeginSession(UpdateVersionRepositoryLayer _)
        {
            yield break;
        }

        #endregion
    }
}