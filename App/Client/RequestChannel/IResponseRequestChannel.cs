using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Observes response listeners</para>
    /// </summary>
    public interface IResponseRequestChannel : IRequestChannel
    {
        /// <summary>
        ///     <para>Adds a listener to this channel.</para>
        /// </summary>
        void RegisterListener(IResponseListener listener);

        /// <summary>
        ///     <para>Removes a listener from this channel.</para>
        /// </summary>
        void UnregisterListener(IResponseListener listener);
    }
    
    /// <summary>
    ///     <para>Listens when channel has received a response.</para>
    /// </summary>
    public interface IResponseListener
    {
        IEnumerator OnResponse(IRequestChannel channel, RequestTask request);
    }
}