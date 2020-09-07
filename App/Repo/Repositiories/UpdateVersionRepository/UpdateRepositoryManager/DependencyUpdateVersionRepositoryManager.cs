using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OregoFramework.Util;

namespace OregoFramework.App
{
    using RepositorySet = HashSet<IUpdateVersionRepository>;
        
    public abstract class DependencyUpdateVersionRepositoryManager : RepoElement,
        IUpdateVersionRepositoryManager
    {
        private readonly Dictionary<Type, RepositorySet> dependencyRepositoryMap;

        public DependencyUpdateVersionRepositoryManager()
        {
            this.dependencyRepositoryMap = new Dictionary<Type, RepositorySet>();
        }

        #region OnPrepare

        protected sealed override void OnPrepare(RepoElement _)
        {
            this.InitDependencyMap();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(DependencyUpdateVersionRepositoryManager _)
        {
        }

        private void InitDependencyMap()
        {
            var repositories = this.repositoryLayer.GetRepositories<IUpdateVersionRepository>();
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
            IUpdateVersionRepository dependencyRepository
        )
        {
            var repositoryDependencies = this.dependencyRepositoryMap[targetRepositoryType];
            repositoryDependencies.Add(dependencyRepository);
        }
        
        public void UnregisterDependency(
            Type targetRepositoryType,
            IUpdateVersionRepository dependencyRepository
        )
        {
            var repositoryDependencies = this.dependencyRepositoryMap[targetRepositoryType];
            repositoryDependencies.Remove(dependencyRepository);
        }

        public IEnumerator UpdateVersionInRepositories()
        {
            var repositories = this.repositoryLayer.GetRepositories<IUpdateVersionRepository>();
            var checkingRepositories = new RepositorySet(repositories);
            while (checkingRepositories.IsNotEmpty())
            {
                var nextRepository = checkingRepositories.First();
                var isUpdatedReference = new Reference<bool>();
                yield return nextRepository.OnUpdateVersionAsync(isUpdatedReference);
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