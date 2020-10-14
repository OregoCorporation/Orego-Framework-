namespace OregoFramework.Game
{
    public interface IEventHandler
    {
        bool isEnabled { get; set; }

        IEvent HandleEvent(IEvent inputEvent);
    }
}