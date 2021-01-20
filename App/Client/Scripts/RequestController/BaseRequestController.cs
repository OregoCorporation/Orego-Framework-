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
        protected IRequestChannel Channel { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Channel = this.Client.Channel;
        }
        
        /// <summary>
        ///     <para>Send unity web request via channel asynchronously.</para>
        /// </summary>
        protected IEnumerator SendRequest(UnityWebRequest webRequest)
        {
            var requestTask = new RequestTask(webRequest);
            yield return this.Channel.SendRequest(requestTask);
        }
    }
}