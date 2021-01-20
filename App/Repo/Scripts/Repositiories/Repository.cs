namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A repository interface.</para>
    ///     <para>Each repository stores one type of data.</para>
    /// </summary>
    public interface IRepository : IRepoElement
    {
    }
    
    /// <summary>
    ///     <para>An repository class.</para>
    /// </summary>
    public abstract class Repository : RepoElement, IRepository
    {
    }
}