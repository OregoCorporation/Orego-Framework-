using System.Collections;

namespace OregoFramework.App
{
    public interface IQueueRequestChannel : IRequestChannel
    {
        IEnumerator Enqueue(RequestTask request);
    }
}