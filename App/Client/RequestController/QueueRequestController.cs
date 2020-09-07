using System.Collections;
using OregoFramework.Util;
using UnityEngine.Networking;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Sends web request via queue.</para>
    /// </summary>
    public abstract class QueueRequestController : BaseRequestController
    {
        protected new QueueRequestChannel channel { get; private set; }

        protected sealed override void OnPrepare(BaseRequestController _)
        {
            this.channel = base.channel.As<QueueRequestChannel>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(QueueRequestController _)
        {
        }

        /// <summary>
        ///     <para>Send unity web request asynchronously via request queue.</para>
        /// </summary>
        protected IEnumerator Enqueue(UnityWebRequest request)
        {
            var requestTask = new RequestTask(request);
            yield return this.channel.Enqueue(requestTask);
        }
    }
}