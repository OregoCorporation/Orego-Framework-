namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>A game component of game system <see cref="IGameContext"/>.</para>
    /// </summary>
    public interface IGameNode
    {
        /// <summary>
        ///     <para>Called when this game component is registered into system.</para>
        /// </summary>
        /// <param name="gameContext">Game system.</param>
        void OnRegistered(IGameContext gameContext);
        
        /// <summary>
        ///     <para>Calls when a game system prepares.
        ///     You can get other game nodes from game context.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPrepareGame(object sender);
        
        /// <summary>
        ///     <para>Calls when a game system readies.
        ///     You can subscribe on other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnReadyGame(object sender);

        /// <summary>
        ///     <para>Calls when game system launches.
        ///     You can start this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnStartGame(object sender);

        /// <summary>
        ///     <para>Calls when game system pauses.
        ///     You can pause this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPauseGame(object sender);

        /// <summary>
        ///     <para>Calls when game system resumes</para>
        ///     You can resume this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnResumeGame(object sender);

        /// <summary>
        ///     <para>Calls when game system finishes</para>
        ///     You can stop this game node and unsubscribe from other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnFinishGame(object sender);
        
        /// <summary>
        ///     <para>Calls when game system destroys.</para>
        ///     You can dispose resources of this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnDestroyGame(object sender);

        /// <summary>
        ///     <para>Called when this game component is unregistered from system.</para>
        /// </summary>
        void OnUnregistered();
    }
}