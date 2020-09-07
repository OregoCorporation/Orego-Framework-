namespace OregoFramework.App
{
    public interface IResetRequestChannel : IRequestChannel
    {
        void RegisterListener(IResetListener listener);

        void UnregisterListener(IResetListener listener);
    }

    public interface IResetListener
    {
        void OnReset(IRequestChannel requestChannel);
    }
}