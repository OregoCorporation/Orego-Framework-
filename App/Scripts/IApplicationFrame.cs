using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///    <para>Architectural application frame with abstract layers.</para>
    /// </summary>
    public interface IApplicationFrame : IRootElement
    {
        /// <summary>
        ///     <para>Works with network.</para>
        /// </summary>
        IClientLayer clientLayer { get; }
        
        /// <summary>
        ///     <para>Works with local storage.</para>
        /// </summary>
        IDatabaseLayer databaseLayer { get; }
        
        /// <summary>
        ///     <para>Works with data logic.</para>
        /// </summary>
        IRepositoryLayer repositoryLayer { get; }

        /// <summary>
        ///     <para>Works with business logic.</para>
        /// </summary>
        IInteractorLayer interactorLayer { get; }
    }
}