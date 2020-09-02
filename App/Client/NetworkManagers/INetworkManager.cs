using System.Collections;

namespace OregoFramework.App
{
    public interface INetworkManager
    {
        IEnumerator SendRequestTask(RequestTask requestTask);

        void Reset();
    }
}