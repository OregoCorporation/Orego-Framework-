using System.Collections.Generic;

namespace OregoFramework.App
{
    public abstract class QueueClient<T> : BaseClient<T> where T : BaseCaller
    {
        private IEnumerable<IOnResponseRequestTaskListener> onResponseListeners;

        private IEnumerable<IOnResetNetworkManagerListener> onResetListeners;

        #region OnCreate

        protected override void OnCreate(BaseClient<T> self)
        {
            base.OnCreate(self);
            this.onResponseListeners = this.ProvideOnResponseRequestTaskListeners();
            this.onResetListeners = this.ProvideOnResetNetworkManagerListeners();
        }

        protected sealed override INetworkManager ProvideNetworkManager()
        {
            return this.ProvideQueueNetworkManager();
        }

        protected abstract QueueNetworkManager ProvideQueueNetworkManager();

        #endregion

        #region OnPrepare

        public override void OnPrepare()
        {
            base.OnPrepare();
            var networkManager = this.GetNetworkManager<QueueNetworkManager>();
            foreach (var listener in this.onResponseListeners)
            {
                networkManager.AddOnResponseListener(listener);
            }

            foreach (var listener in this.onResetListeners)
            {
                networkManager.AddOnResetListener(listener);
            }
        }

        protected virtual IEnumerable<IOnResponseRequestTaskListener>
            ProvideOnResponseRequestTaskListeners()
        {
            return new IOnResponseRequestTaskListener[] { };
        }

        protected virtual IEnumerable<IOnResetNetworkManagerListener>
            ProvideOnResetNetworkManagerListeners()
        {
            return new IOnResetNetworkManagerListener[] { };
        }

        #endregion
    }
}