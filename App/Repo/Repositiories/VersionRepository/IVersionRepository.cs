using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    public interface IVersionRepository : IRepository
    {
        IEnumerator OnUpdateVersion(Reference<bool> isUpdated);
    }
}