namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Observes reset listeners.</para>
    ///     <para>Can reset its state.</para>
    /// </summary>
    public interface IResetRequestChannel : IRequestChannel
    {
        /// <summary>
        ///     <para>Adds a listener on this channel.</para>
        /// </summary>
        void RegisterListener(IResetListener listener);

        /// <summary>
        ///     <para>Removes a listener from this channel.</para>
        /// </summary>
        void UnregisterListener(IResetListener listener);

        /// <summary>
        ///     <para>Resets channel state.</para>
        /// </summary>
        void Reset();
    }

    /// <summary>
    ///     <para>Listens when channel was reset.</para>
    /// </summary>
    public interface IResetListener
    {
        void OnReset(IRequestChannel requestChannel);
    }
}