using System.Collections;
using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRequestChannel"/>
    public abstract class RequestChannel : Element, IRequestChannel
    {
        /// <inheritdoc cref="IRequestChannel.Send"/>
        public IEnumerator Send(RequestTask request)
        {
            yield return OnBeforeRequest(request);
            yield return request.unityWebRequest.SendWebRequest();
            yield return this.OnAfterRequest(request);
        }

        /// <summary>
        ///     <para>Called before sending a request.</para>
        /// </summary>
        protected virtual IEnumerator OnBeforeRequest(RequestTask request)
        {
            yield break;
        }

        /// <summary>
        ///     <para>Called after a request has sent.</para>
        /// </summary>
        protected virtual IEnumerator OnAfterRequest(RequestTask request)
        {
            yield break;
        }
    }
}