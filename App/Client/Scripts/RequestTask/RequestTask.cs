using System;
using UnityEngine;
using UnityEngine.Networking;

namespace OregoFramework.App
{
    /// <summary>
    ///     Unity web request with state.
    /// </summary>
    public sealed class RequestTask
    {
        public UnityWebRequest unityWebRequest { get; }

        public State state { get; set; }

        public RequestTask(UnityWebRequest webRequest)
        {
            this.state = State.PENDING;
            this.unityWebRequest = webRequest;
        }

        /// <summary>
        ///     <para>Request state.</para>
        /// </summary>
        public enum State
        {
            PENDING,
            PROCESSING,
            CANCELED,
            FINISHED
        }
    }

    public static class Extensions
    {
        public static string GetBodyString(this RequestTask requestTask)
        {
            var currentWebRequest = requestTask.unityWebRequest;
            var downloadHandler = currentWebRequest.downloadHandler;
            return downloadHandler.text;
        }

        public static long GetResponseCode(this RequestTask requestTask)
        {
            var currentWebRequest = requestTask.unityWebRequest;
            return currentWebRequest.responseCode;
        }

        public static bool DeserializeBody<T>(this RequestTask requestTask, out T result)
        {
            try
            {
                var bodyString = requestTask.GetBodyString();
                result = JsonUtility.FromJson<T>(bodyString);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static void SetPending(this RequestTask requestTask)
        {
            requestTask.state = RequestTask.State.PENDING;
        }

        public static bool IsPending(this RequestTask requestTask)
        {
            return requestTask.state == RequestTask.State.PENDING;
        }

        public static void SetProcessing(this RequestTask requestTask)
        {
            requestTask.state = RequestTask.State.PROCESSING;
        }

        public static bool IsProcessing(this RequestTask request)
        {
            return request.state == RequestTask.State.PROCESSING;
        }

        public static void SetCanceled(this RequestTask requestTask)
        {
            requestTask.state = RequestTask.State.CANCELED;
        }

        public static bool IsCanceled(this RequestTask request)
        {
            return request.state == RequestTask.State.CANCELED;
        }

        public static void SetFinished(this RequestTask requestTask)
        {
            requestTask.state = RequestTask.State.FINISHED;
        }

        public static bool IsFinished(this RequestTask request)
        {
            return request.state == RequestTask.State.FINISHED;
        }
    }
}