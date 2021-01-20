using System.Collections;
using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Check for updates user data.</para>
    /// </summary>
    public interface IUpdateDataSystem : IRepoElement
    {
        /// <summary>
        ///     <para>Updates data in repository layer asynchronously.</para>
        /// </summary>
        IEnumerator CheckForUpdates();
    }
    
    /// <inheritdoc cref="IUpdateDataSystem"/>
    /// <summary>
    ///     <para>Check for updates user data through data handlers.</para>
    ///     <para>Keeps data handlers.</para>
    /// </summary>
    /// <typeparam name="T">Base data handler class.</typeparam>
    public abstract class UpdateDataSystem<T> : RepoElement, IUpdateDataSystem
        where T : IUpdateDataHandler
    {
        /// <summary>
        ///     <para>Use this handlers for update user data in <see cref="CheckForUpdates"/>.</para>
        /// </summary>
        protected IEnumerable<T> Handlers { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Handlers = this.CreateElements<T>();
        }
        
        /// <inheritdoc cref="IUpdateDataSystem.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates();
    }
}