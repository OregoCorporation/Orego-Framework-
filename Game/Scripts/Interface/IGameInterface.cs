namespace OregoFramework.Game
{
    public interface IGameInterface
    {
        IGameContext currentGameContext { get; }
        
        void BindGameContext(IGameContext gameContext);

        void UnbindGameContext();
    }
}