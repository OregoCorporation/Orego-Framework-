using System.Collections;

namespace OregoFramework.App
{
    public interface IResponseRequestChannel : IRequestChannel
    {
        void RegisterListener(IResponseListener listener);

        void UnregisterListener(IResponseListener listener);
    }

    public interface IResponseListener
    {
        IEnumerator OnResponse(IRequestChannel channel, RequestTask request);
    }
}