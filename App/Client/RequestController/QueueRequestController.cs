using System.Collections;
using OregoFramework.Util;
using UnityEngine.Networking;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Sends web request via queue channel.</para>
    ///     <para>Required <see cref="QueueRequestChannel"/> in client.</para>
    /// </summary>
    public abstract class QueueRequestController : BaseRequestController
    {
        /// <inheritdoc cref="BaseRequestController.channel"/>
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
        ///     <para>Send unity web request asynchronously via queue channel.</para>
        /// </summary>
        protected IEnumerator EnqueueRequest(UnityWebRequest request)
        {
            var requestTask = new RequestTask(request);
            yield return this.channel.EnqueueRequest(requestTask);
        }
    }
}