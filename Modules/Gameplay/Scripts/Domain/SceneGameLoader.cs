using System.Collections;
using Gullis;
using OregoFramework.App;

namespace OregoFramework.Module
{
    public abstract class SceneGameLoader : DomainElement, ISceneLoader
    {
        public abstract IEnumerator OnLoadScene(object sender, ISceneGameContext context);
    }
}