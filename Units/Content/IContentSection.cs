using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public interface IContentSection : IElement
    {
        IEnumerator LoadResources();
    }
}