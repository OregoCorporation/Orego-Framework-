using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class Content : Element, IContent
    {
        public abstract IEnumerator LoadResources();
    }
}