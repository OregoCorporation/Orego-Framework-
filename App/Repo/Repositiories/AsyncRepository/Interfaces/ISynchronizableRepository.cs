using System;
using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public interface ISynchronizableRepository<out T> : IAsyncRepository
    {
        event Action<T> OnDataSynchronizedEvent;

        IEnumerator SynchronizeDataAsync(Reference<bool> isSuccess);
    }
}