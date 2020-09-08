using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Channel with request queue mechanism.</para>
    /// </summary>
    public interface IQueueRequestChannel : IRequestChannel
    {
        /// <summary>
        ///     <para>Sends a request in the order of the queue asynchronously.</para>
        /// </summary>
        /// 
        /// <param name="request">A request instance.</param>
        IEnumerator Enqueue(RequestTask request);
    }
}