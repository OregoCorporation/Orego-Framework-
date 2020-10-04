using System.Collections;
using UnityEngine.Networking;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Sends web requests.</para>
    /// </summary>
    public abstract class BaseRequestController : RequestController<IBaseClient>
    {
        /// <summary>
        ///     <para>A client channel reference.</para>
        /// </summary>
        protected IRequestChannel channel { get; private set; }

        protected sealed override void OnPrepare(RequestController<IBaseClient> _)
        {
            this.channel = this.client.channel;
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(BaseRequestController _)
        {
        }

        /// <summary>
        ///     <para>Send unity web request via channel asynchronously.</para>
        /// </summary>
        protected IEnumerator SendRequest(UnityWebRequest webRequest)
        {
            var requestTask = new RequestTask(webRequest);
            yield return this.channel.SendRequest(requestTask);
        }
    }
}