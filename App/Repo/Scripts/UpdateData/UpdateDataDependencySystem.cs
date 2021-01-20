using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elementary;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Asynchronously updates user data through handler dependency graph.</para>
    /// </summary>
    public abstract class UpdateDataDependencySystem<T> : UpdateDataSystem<T>
        where T : IUpdateDataHandler
    {
        /// <summary>
        ///     <para>Keeps handler dependencies of each other.</para>
        /// </summary>
        private readonly Dictionary<Type, HashSet<T>> DependencyHandlerMatrix;

        public UpdateDataDependencySystem()
        {
            this.DependencyHandlerMatrix = new Dictionary<Type, HashSet<T>>();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            foreach (var handler in this.Handlers)
            {
                var handlerType = handler.GetType();
                var dependencyHandlers = new HashSet<T>();
                this.DependencyHandlerMatrix.Add(handlerType, dependencyHandlers);
            }
        }
        
        /// <summary>
        ///     <para>Adds dependent handler on target handler.</para>
        /// </summary>
        /// <param name="dependency">Dependent handler.</param>
        /// <typeparam name="H">Target handler.</typeparam>
        public void RegisterDependency<H>(T dependency)
        {
            var dependencies = this.DependencyHandlerMatrix[typeof(H)];
            dependencies.Add(dependency);
        }

        /// <summary>
        ///     <para>Remove dependent handler from target handler.</para>
        /// </summary>
        /// <param name="dependency">Dependent handler.</param>
        /// <typeparam name="H">Target handler.</typeparam>
        public void UnregisterDependency<H>(T dependency)
        {
            var dependencies = this.DependencyHandlerMatrix[typeof(H)];
            dependencies.Remove(dependency);
        }
        
        public sealed override IEnumerator CheckForUpdates()
        {
            var checkForUpdatesHandlers = new HashSet<T>(this.Handlers);
            while (checkForUpdatesHandlers.IsNotEmpty())
            {
                var nextHanlder = checkForUpdatesHandlers.First();
                checkForUpdatesHandlers.Remove(nextHanlder);
                var isUpdated = new Reference<bool>();
                yield return nextHanlder.CheckForUpdates(isUpdated);
                if (!isUpdated.value)
                {
                    continue;
                }

                var updatedRepositoryType = nextHanlder.GetType();
                var dependencyRepositories = this.DependencyHandlerMatrix[updatedRepositoryType];
                checkForUpdatesHandlers.UnionWith(dependencyRepositories);
            }
        }
    }
}