using System.Collections;

namespace OregoFramework.App
{
    public interface IQueueNetworkManager : INetworkManager
    {
        IEnumerator EnqueueRequestTask(RequestTask requestTask);
    }
}