using System.Collections;
using Elementary;

namespace OregoFramework.Module
{
    public interface IContent : IElement
    {
        IEnumerator PreloadResources();
    }
    
    public abstract class Content : Element, IContent
    {
        public virtual IEnumerator PreloadResources()
        {
            yield break;
        }
    }
}