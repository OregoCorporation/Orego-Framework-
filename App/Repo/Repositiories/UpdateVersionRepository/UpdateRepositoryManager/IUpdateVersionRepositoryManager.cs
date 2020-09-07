using System.Collections;

namespace OregoFramework.App
{
    public interface IUpdateVersionRepositoryManager : IRepoElement
    {
        IEnumerator UpdateVersionInRepositories();
    }
}