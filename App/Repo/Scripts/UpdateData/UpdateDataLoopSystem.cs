using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Asynchronously updates user data through handlers one by one.</para>
    /// </summary>
    public abstract class UpdateDataLoopSystem<T> : UpdateDataSystem<T> where T : IUpdateDataHandler
    {
       /// <inheritdoc cref="IUpdateDataSystem.CheckForUpdates"/>
        public sealed override IEnumerator CheckForUpdates()
        {
            var isCheckForUpdatesRequired = true;
            while (isCheckForUpdatesRequired)
            {
                isCheckForUpdatesRequired = false;
                foreach (var handler in this.handlers)
                {
                    var isUpdated = new Reference<bool>();
                    yield return handler.CheckForUpdates(isUpdated);
                    if (isUpdated.value)
                    {
                        isCheckForUpdatesRequired = true;
                        break;
                    }
                }
            }
        }
    }
}