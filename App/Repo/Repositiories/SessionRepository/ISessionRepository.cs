using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Listens user session events.</para>
    /// </summary>
    public interface ISessionRepository : IRepository
    {
        /// <summary>
        ///     <para>Called when a user session is starting.</para>
        /// </summary>
        IEnumerator OnBeginSession();
        
        /// <summary>
        ///     <para>Called when a user session is finishing.</para>
        /// </summary>
        IEnumerator OnEndSession();
    }
}