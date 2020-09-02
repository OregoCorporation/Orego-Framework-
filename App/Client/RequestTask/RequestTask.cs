using UnityEngine.Networking;

namespace OregoFramework.App
{
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
    
    public enum RequestState
    {
        PENDING,
        PROCESSING,
        CANCELED,
        FINISHED
    }
}