using UnityEngine.Networking;

namespace OregoFramework.App
{
    /// <summary>
    ///     Unity web request wrapper with state.
    /// </summary>
    public sealed class RequestTask
    {
        public UnityWebRequest unityWebRequest { get; }

        public RequestState state { get; set; }

        public RequestTask(UnityWebRequest webRequest)
        {
            this.state = RequestState.PENDING;
            this.unityWebRequest = webRequest;
        }
    }
    
    /// <summary>
    ///      Request state.
    /// </summary>
    public enum RequestState
    {
        PENDING,
        PROCESSING,
        CANCELED,
        FINISHED
    }
}