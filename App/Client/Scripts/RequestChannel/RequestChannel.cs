using System.Collections;
using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRequestChannel"/>
    public abstract class RequestChannel : Element, IRequestChannel
    {
        /// <inheritdoc cref="IRequestChannel.SendRequest"/>
        public IEnumerator SendRequest(RequestTask request)
        {
            yield return OnBeforeSendRequest(request);
            yield return request.unityWebRequest.SendWebRequest();
            yield return this.OnAfterSendRequest(request);
        }

        /// <summary>
        ///     <para>Called before sending a request.</para>
        /// </summary>
        protected virtual IEnumerator OnBeforeSendRequest(RequestTask request)
        {
            yield break;
        }

        /// <summary>
        ///     <para>Called after a request has sent.</para>
        /// </summary>
        protected virtual IEnumerator OnAfterSendRequest(RequestTask request)
        {
            yield break;
        }
    }
}