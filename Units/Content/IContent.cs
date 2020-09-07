using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public interface IContent : IElement
    {
        IEnumerator LoadResources();
    }
}