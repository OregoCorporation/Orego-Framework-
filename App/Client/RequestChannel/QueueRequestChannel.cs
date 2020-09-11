using System.Collections;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A base class with queue request mechanism.</para>
    /// </summary>
    public abstract class QueueRequestChannel : BaseRequestChannel, IQueueRequestChannel
    {
        /// <summary>
        ///     <para>Queue of pending requests.</para>
        /// </summary>
        private readonly Queue<RequestTask> requestQueue;

        /// <summary>
        /// <para>Sending request.</para>
        /// </summary>
        private RequestTask currentRequest;

        protected QueueRequestChannel()
        {
            this.requestQueue = new Queue<RequestTask>();
        }

        #region Enqueue

        /// <inheritdoc cref="IQueueRequestChannel.Enqueue"/>
        public IEnumerator Enqueue(RequestTask request)
        {
            if (this.currentRequest != null)
            {
                this.requestQueue.Enqueue(request);
                yield return new WaitWhile(request.IsPending);
                if (!request.IsProcessing())
                {
                    yield break;
                }
            }

            this.currentRequest = request;
            yield return this.Send(request);
            request.SetFinished();
            if (this.requestQueue.IsNotEmpty())
            {
                var nextRequest = this.requestQueue.Dequeue();
                nextRequest.SetProcessing();
            }
            else
            {
                this.currentRequest = null;
            }
        }

        #endregion

        #region Reset

        protected sealed override void OnReset(BaseRequestChannel _)
        {
            this.CancelRequests();
            this.OnReset(this);
        }

        private void CancelRequests()
        {
            if (this.currentRequest == null)
            {
                return;
            }

            this.currentRequest.SetCanceled();
            while (this.requestQueue.IsNotEmpty())
            {
                var request = this.requestQueue.Dequeue();
                request.SetCanceled();
            }

            this.currentRequest = null;
        }

        protected virtual void OnReset(QueueRequestChannel _)
        {
        }

        #endregion
    }
}