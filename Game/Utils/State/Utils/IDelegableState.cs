namespace OregoFramework.Util
{
    public interface IDelegableState : IState 
    {
        void OnProvideParent(object parent);
    }
}