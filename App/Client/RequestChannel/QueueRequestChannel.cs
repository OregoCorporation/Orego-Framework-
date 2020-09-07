using System.Collections;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.App
{
    public abstract class QueueRequestChannel : BaseRequestChannel, IQueueRequestChannel
    {
        private readonly Queue<RequestTask> requestTaskQueue;

        private RequestTask currentRequestTask;

        protected QueueRequestChannel()
        {
            this.requestTaskQueue = new Queue<RequestTask>();
        }

        #region Enqueue

        public IEnumerator Enqueue(RequestTask request)
        {
            if (this.currentRequestTask != null)
            {
                this.requestTaskQueue.Enqueue(request);
                yield return new WaitWhile(request.IsPending);
                if (!request.IsProcessing())
                {
                    yield break;
                }
            }

            this.currentRequestTask = request;
            yield return this.Send(request);
            request.SetFinished();
            if (this.requestTaskQueue.IsNotEmpty())
            {
                var nextRequest = this.requestTaskQueue.Dequeue();
                nextRequest.SetProcessing();
            }
            else
            {
                this.currentRequestTask = null;
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
            if (this.currentRequestTask == null)
            {
                return;
            }

            this.currentRequestTask.SetCanceled();
            while (this.requestTaskQueue.IsNotEmpty())
            {
                var request = this.requestTaskQueue.Dequeue();
                request.SetCanceled();
            }

            this.currentRequestTask = null;
        }

        protected virtual void OnReset(QueueRequestChannel _)
        {
        }

        #endregion
    }
}