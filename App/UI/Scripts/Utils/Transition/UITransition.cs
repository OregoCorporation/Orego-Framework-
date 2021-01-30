namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A transition args when state changes.</para>
    /// </summary>
    public interface IUITransition
    {
    }

    public interface IUITransitionable
    {
        void OnLoaded(object sender, IUITransition transition = null);

        void OnUnload(object sender);
    }
}