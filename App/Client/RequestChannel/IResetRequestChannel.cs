namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Channel with reset listeners.</para>
    /// </summary>
    public interface IResetRequestChannel : IRequestChannel
    {
        /// <summary>
        ///     <para>Adds a listener to this channel.</para>
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