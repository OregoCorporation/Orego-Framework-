using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Sends requests on low level.</para>
    /// </summary>
    public interface IRequestChannel
    {
        /// <summary>
        ///     <para>Sends a request asynchronously.</para>
        /// </summary>
        /// 
        /// <param name="request">A request instance.</param>
        IEnumerator Send(RequestTask request);
    }
}