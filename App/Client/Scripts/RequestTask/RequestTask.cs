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
        public UnityWebRequest UnityWebRequest { get; }

        public RequestState State { get; set; }

        public RequestTask(UnityWebRequest webRequest)
        {
            this.State = RequestState.PENDING;
            this.UnityWebRequest = webRequest;
        }
    }

    public static class Extensions
    {
        public static string GetBodyString(this RequestTask requestTask)
        {
            var currentWebRequest = requestTask.UnityWebRequest;
            var downloadHandler = currentWebRequest.downloadHandler;
            return downloadHandler.text;
        }

        public static long GetResponseCode(this RequestTask requestTask)
        {
            var currentWebRequest = requestTask.UnityWebRequest;
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
            requestTask.State = RequestState.PENDING;
        }

        public static bool IsPending(this RequestTask requestTask)
        {
            return requestTask.State == RequestState.PENDING;
        }

        public static void SetProcessing(this RequestTask requestTask)
        {
            requestTask.State = RequestState.PROCESSING;
        }

        public static bool IsProcessing(this RequestTask request)
        {
            return request.State == RequestState.PROCESSING;
        }

        public static void SetCanceled(this RequestTask requestTask)
        {
            requestTask.State = RequestState.CANCELED;
        }

        public static bool IsCanceled(this RequestTask request)
        {
            return request.State == RequestState.CANCELED;
        }

        public static void SetFinished(this RequestTask requestTask)
        {
            requestTask.State = RequestState.FINISHED;
        }

        public static bool IsFinished(this RequestTask request)
        {
            return request.State == RequestState.FINISHED;
        }
    }
}