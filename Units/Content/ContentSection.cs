using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class ContentSection : Element, IContentSection
    {
        public abstract IEnumerator LoadResources();
    }
}