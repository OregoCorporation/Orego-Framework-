using System.Collections;

namespace OregoFramework.App
{
    public interface IAsyncRepository : IRepository
    {
        IEnumerator OnBeginSession();

        IEnumerator OnEndSession();
    }
}