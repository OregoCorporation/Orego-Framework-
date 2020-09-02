namespace OregoFramework.App
{
    public interface IResetObserverNetworkManager : INetworkManager
    {
        void AddOnResetListener(IOnResetNetworkManagerListener listener);

        void RemoveOnResetListener(IOnResetNetworkManagerListener listener);
    }

    public interface IOnResetNetworkManagerListener
    {
        void OnReset(INetworkManager networkManager);
    }
}