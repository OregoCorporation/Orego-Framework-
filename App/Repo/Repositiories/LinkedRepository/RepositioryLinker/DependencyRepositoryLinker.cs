using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OregoFramework.Util;

namespace OregoFramework.App
{
    using RepositorySet = HashSet<ILinkedRepository>;

    /// <summary>
    ///     <para>Links repository data by dependency map until all repositories will resolve.</para>
    /// </summary>
    public abstract class DependencyRepositoryLinker : RepoElement, IRepositoryLinker
    {
        /// <summary>
        ///     <para>Dependency dictionary of repositories.</para>
        ///     <para>Key: target repository type, value: dependent repositories from target.</para>
        /// </summary>
        private readonly Dictionary<Type, RepositorySet> dependencyRepositoryMap;

        public DependencyRepositoryLinker()
        {
            this.dependencyRepositoryMap = new Dictionary<Type, RepositorySet>();
        }

        protected sealed override void OnPrepare(RepoElement _)
        {
            this.InitDependencyMap();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(DependencyRepositoryLinker _)
        {
        }

        /// <summary>
        ///     <para>Initializes dependency dictionary.</para>
        /// </summary>
        private void InitDependencyMap()
        {
            var repositories = this.repositoryLayer.GetRepositories<ILinkedRepository>();
            foreach (var repository in repositories)
            {
                var repositoryType = repository.GetType();
                var dependencyRepositories = new RepositorySet();
                this.dependencyRepositoryMap.Add(repositoryType, dependencyRepositories);
            }
        }
        
        /// <summary>
        ///     <para>Adds dependency repository on target repository.</para>
        /// </summary>
        /// 
        /// <param name="dependencyRepository">Dependent repository.</param>
        /// <typeparam name="T">Target repository.</typeparam>
        public void RegisterDependency<T>(ILinkedRepository dependencyRepository)
        {
            var repositoryDependencies = this.dependencyRepositoryMap[typeof(T)];
            repositoryDependencies.Add(dependencyRepository);
        }

        /// <summary>
        ///     <para>Removes dependency repository from target repository.</para>
        /// </summary>
        /// 
        /// <param name="dependencyRepository">Dependent repository.</param>
        /// <typeparam name="T">Target repository.</typeparam>
        public void UnregisterDependency<T>(ILinkedRepository dependencyRepository)
        {
            var repositoryDependencies = this.dependencyRepositoryMap[typeof(T)];
            repositoryDependencies.Remove(dependencyRepository);
        }

        /// <inheritdoc cref="IRepositoryLinker.ResolveDataInRepositories"/>
        public IEnumerator ResolveDataInRepositories()
        {
            var repositories = this.repositoryLayer.GetRepositories<ILinkedRepository>();
            var notResolvedRepositories = new RepositorySet(repositories);
            while (notResolvedRepositories.IsNotEmpty())
            {
                var nextRepository = notResolvedRepositories.First();
                var isResolved = new Reference<bool>();
                yield return nextRepository.ResolveLinkedData(isResolved);
                if (!isResolved.value)
                {
                    continue;
                }

                var updatedRepositoryType = nextRepository.GetType();
                var dependencyRepositories = this.dependencyRepositoryMap[updatedRepositoryType];
                notResolvedRepositories.UnionWith(dependencyRepositories);
            }
        }
    }
}