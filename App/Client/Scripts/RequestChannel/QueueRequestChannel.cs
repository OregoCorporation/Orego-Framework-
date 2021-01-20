using System.Collections;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

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
        IEnumerator EnqueueRequest(RequestTask request);
    }
    
    /// <summary>
    ///     <para>A base class with queue request mechanism.</para>
    /// </summary>
    public abstract class QueueRequestChannel : BaseRequestChannel, IQueueRequestChannel
    {
        /// <summary>
        ///     <para>Queue of pending requests.</para>
        /// </summary>
        private readonly Queue<RequestTask> pendingRequests;

        /// <summary>
        /// <para>Sending request.</para>
        /// </summary>
        private RequestTask currentRequest;

        protected QueueRequestChannel()
        {
            this.pendingRequests = new Queue<RequestTask>();
        }

        /// <inheritdoc cref="IQueueRequestChannel.EnqueueRequest"/>
        public IEnumerator EnqueueRequest(RequestTask request)
        {
            if (this.currentRequest != null)
            {
                this.pendingRequests.Enqueue(request);
                yield return new WaitWhile(request.IsPending);
                if (!request.IsProcessing())
                {
                    yield break;
                }
            }

            this.currentRequest = request;
            yield return this.SendRequest(request);
            request.SetFinished();
            if (this.pendingRequests.IsNotEmpty())
            {
                var nextRequest = this.pendingRequests.Dequeue();
                nextRequest.SetProcessing();
            }
            else
            {
                this.currentRequest = null;
            }
        }

        public override void Reset()
        {
            base.Reset();
            this.CancelAllRequests();
        }
        
        private void CancelAllRequests()
        {
            if (this.currentRequest == null)
            {
                return;
            }

            this.currentRequest.SetCanceled();
            while (this.pendingRequests.IsNotEmpty())
            {
                var request = this.pendingRequests.Dequeue();
                request.SetCanceled();
            }

            this.currentRequest = null;
        }
    }
}