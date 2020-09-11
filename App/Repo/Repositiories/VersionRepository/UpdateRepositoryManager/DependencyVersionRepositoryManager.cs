using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OregoFramework.Util;

namespace OregoFramework.App
{
    using RepositorySet = HashSet<IVersionRepository>;
        
    public abstract class DependencyVersionRepositoryManager : RepoElement,
        IVersionRepositoryManager
    {
        private readonly Dictionary<Type, RepositorySet> dependencyRepositoryMap;

        public DependencyVersionRepositoryManager()
        {
            this.dependencyRepositoryMap = new Dictionary<Type, RepositorySet>();
        }

        #region OnPrepare

        protected sealed override void OnPrepare(RepoElement _)
        {
            this.InitDependencyMap();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(DependencyVersionRepositoryManager _)
        {
        }

        private void InitDependencyMap()
        {
            var repositories = this.repositoryLayer.GetRepositories<IVersionRepository>();
            foreach (var repository in repositories)
            {
                var repositoryType = repository.GetType();
                var dependencyRepositories = new RepositorySet();
                this.dependencyRepositoryMap.Add(repositoryType, dependencyRepositories);
            }
        }

        #endregion

        public void RegisterDependency(
            Type targetRepositoryType,
            IVersionRepository dependencyRepository
        )
        {
            var repositoryDependencies = this.dependencyRepositoryMap[targetRepositoryType];
            repositoryDependencies.Add(dependencyRepository);
        }
        
        public void UnregisterDependency(
            Type targetRepositoryType,
            IVersionRepository dependencyRepository
        )
        {
            var repositoryDependencies = this.dependencyRepositoryMap[targetRepositoryType];
            repositoryDependencies.Remove(dependencyRepository);
        }

        public IEnumerator UpdateVersionInRepositories()
        {
            var repositories = this.repositoryLayer.GetRepositories<IVersionRepository>();
            var checkingRepositories = new RepositorySet(repositories);
            while (checkingRepositories.IsNotEmpty())
            {
                var nextRepository = checkingRepositories.First();
                var isUpdatedReference = new Reference<bool>();
                yield return nextRepository.OnUpdateVersion(isUpdatedReference);
                if (!isUpdatedReference.value)
                {
                    continue;
                }

                var updatedRepositoryType = nextRepository.GetType();
                var dependencyRepositories = this.dependencyRepositoryMap[updatedRepositoryType];
                checkingRepositories.AddRange(dependencyRepositories);
            }
        }
    }
}