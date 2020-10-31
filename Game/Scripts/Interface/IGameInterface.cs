namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Provides gameplay interface with player.
    ///     Each interface is responsible for single interaction.</para>
    /// </summary>
    public interface IGameInterface
    {
        /// <summary>
        ///     <para>Called when this interface is registered into system.</para>
        /// </summary>
        /// <param name="system">interface system.</param>
        void OnRegistered(IGameInterfaceSystem system);
        
        /// <summary>
        ///     <para>Calls when a game system prepares.
        ///     You can get game nodes from game context.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGamePrepared(object sender);

        /// <summary>
        ///     <para>Calls when a game system readies.
        ///     You can subscribe on game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameReady(object sender);
        
        /// <summary>
        ///     <para>Calls when game system launches.
        ///     You can start this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameStarted(object sender);

        /// <summary>
        ///     <para>Calls when game system pauses.
        ///     You can pause this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGamePaused(object sender);
        
        /// <summary>
        ///     <para>Calls when game system resumes.
        ///     You can resume this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameResumed(object sender);

        /// <summary>
        ///     <para>Calls when game system finishes.</para>
        ///     You can stop this interface and unsubscribe from other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameFinished(object sender);

        /// <summary>
        ///     <para>Called when this interface is unregistered from system.</para>
        /// </summary>
        void OnUnregistered();
    }
}