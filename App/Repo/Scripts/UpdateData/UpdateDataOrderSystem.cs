using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Asynchronously updates user data by order.</para>
    /// </summary>
    public abstract class UpdateDataOrderSystem<T> : UpdateDataSystem<T>
        where T : IUpdateDataHandler
    {
        /// <inheritdoc cref="IUpdateDataSystem.CheckForUpdates"/>
        public sealed override IEnumerator CheckForUpdates()
        {
            foreach (var handler in this.Handlers)
            {
                var isUpdated = new Reference<bool>();
                yield return handler.CheckForUpdates(isUpdated);
            }
        }
    }
}