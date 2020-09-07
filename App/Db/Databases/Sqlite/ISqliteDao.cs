#if SQL
using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Queries a sqlite database.</para>
    /// </summary>
    public interface ISqliteDao : IDao
    {
        /// <summary>
        ///     <para>Called when application has connected to sqlite database.</para>
        /// </summary>
        IEnumerator OnConnect();
    }
}
#endif