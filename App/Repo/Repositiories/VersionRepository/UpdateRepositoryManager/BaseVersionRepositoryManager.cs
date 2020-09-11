using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class BaseVersionRepositoryManager : RepoElement, IVersionRepositoryManager
    {
        public IEnumerator UpdateVersionInRepositories()
        {
            var repositories = this.repositoryLayer.GetRepositories<IVersionRepository>();
            var isRequiredUpdate = true;
            while (isRequiredUpdate)
            {
                isRequiredUpdate = false;
                foreach (var repository in repositories)
                {
                    var isUpdatedReference = new Reference<bool>();
                    yield return repository.OnUpdateVersion(isUpdatedReference);
                    if (isUpdatedReference.value)
                    {
                        isRequiredUpdate = true;
                        break;
                    }
                }
            }
        }
    }
}