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
        /// <inheritdoc cref="BaseRequestController.Channel"/>
        protected new QueueRequestChannel Channel { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Channel = base.Channel.As<QueueRequestChannel>();
        }

        /// <summary>
        ///     <para>Send unity web request asynchronously via queue channel.</para>
        /// </summary>
        protected IEnumerator EnqueueRequest(UnityWebRequest request)
        {
            var requestTask = new RequestTask(request);
            yield return this.Channel.EnqueueRequest(requestTask);
        }
    }
}