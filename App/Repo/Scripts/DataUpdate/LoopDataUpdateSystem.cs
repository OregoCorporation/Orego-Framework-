using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Asynchronously updates user data through handlers one by one.</para>
    /// </summary>
    public abstract class LoopDataUpdateSystem<T> : DataUpdateSystem<T> where T : IDataUpdateHandler
    {
       /// <inheritdoc cref="IDataUpdateSystem.CheckForUpdates"/>
        public sealed override IEnumerator CheckForUpdates()
        {
            var isCheckForUpdatesRequired = true;
            while (isCheckForUpdatesRequired)
            {
                isCheckForUpdatesRequired = false;
                foreach (var handler in this.Handlers)
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