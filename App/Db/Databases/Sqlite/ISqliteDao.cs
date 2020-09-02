#if SQL
using System.Collections;

namespace OregoFramework.App
{
    public interface ISqliteDao : IDao
    {
        IEnumerator OnConnect();
    }
}
#endif