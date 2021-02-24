namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A transition args when state changes.</para>
    /// </summary>
    public interface IUIStateTransition
    {
    }

    public interface IUIStateable
    {
        void OnEnter(object sender, IUIStateTransition transition = null);

        void OnExit(object sender);
    }
}