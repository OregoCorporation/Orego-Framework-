using System.Collections;
using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
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
        protected IEnumerable<T> handlers { get; private set; }

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            this.handlers = this.CreateElements<T>();
            this.OnCreate(this, context);
        }

        protected virtual void OnCreate(UpdateDataSystem<T> _, IElementContext context)
        {
        }

        /// <inheritdoc cref="IUpdateDataSystem.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates();
    }
}