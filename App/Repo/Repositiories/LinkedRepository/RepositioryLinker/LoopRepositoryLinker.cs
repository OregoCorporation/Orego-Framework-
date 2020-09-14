using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Links repository data by loop until all repositories will resolve.</para>
    /// </summary>
    public abstract class LoopRepositoryLinker : RepoElement, IRepositoryLinker
    {
        /// <inheritdoc cref="IRepositoryLinker.ResolveDataInRepositories"/>
        public IEnumerator ResolveDataInRepositories()
        {
            var repositories = this.repositoryLayer.GetRepositories<ILinkedRepository>();
            var isRequiredResolve = true;
            while (isRequiredResolve)
            {
                isRequiredResolve = false;
                foreach (var repository in repositories)
                {
                    var isResolved = new Reference<bool>();
                    yield return repository.ResolveLinkedData(isResolved);
                    if (isResolved.value)
                    {
                        isRequiredResolve = true;
                        break;
                    }
                }
            }
        }
    }
}