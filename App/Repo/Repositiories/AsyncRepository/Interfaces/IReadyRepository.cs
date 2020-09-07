using System;

namespace OregoFramework.App
{
    public interface IReadyRepository<out T> : IAsyncRepository
    {
        event Action<T> OnDataReadyEvent;
    }
}