using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public interface IUpdateVersionRepository : IRepository
    {
        IEnumerator OnUpdateVersionAsync(Reference<bool> isUpdated);
    }
}