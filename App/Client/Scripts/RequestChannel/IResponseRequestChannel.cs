using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Observes response listeners</para>
    /// </summary>
    public interface IResponseRequestChannel : IRequestChannel
    {
        /// <summary>
        ///     <para>Adds a listener on this channel.</para>
        /// </summary>
        void RegisterListener(IResponseRequestListener listener);

        /// <summary>
        ///     <para>Removes a listener from this channel.</para>
        /// </summary>
        void UnregisterListener(IResponseRequestListener listener);
    }
    
    /// <summary>
    ///     <para>Listens when channel has received a response.</para>
    /// </summary>
    public interface IResponseRequestListener
    {
        IEnumerator OnResponse(IRequestChannel channel, RequestTask request);
    }
}