using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A repository which depends of other repositories.</para>
    /// </summary>
    public interface ILinkedRepository : IRepository
    {
        /// <summary>
        ///     <para>Updates its user data block depending on data blocks of other repositories.</para>
        /// </summary>
        /// 
        /// <param name="isResolved">Boolean reference. Put a boolean result into reference.</param>
        /// <returns>Was update or not.</returns>
        IEnumerator ResolveLinkedData(Reference<bool> isResolved);
    }
}