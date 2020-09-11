using System.Collections;

namespace OregoFramework.App
{
        
    
    public interface IVersionRepositoryManager : IRepoElement
    {
        IEnumerator UpdateVersionInRepositories();
    }
}