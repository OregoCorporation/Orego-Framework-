using System.Collections;
using GameElementary;
using OregoFramework.App;

namespace OregoFramework.Module
{
    public abstract class SceneGameLoader : DomainElement, ISceneLoader
    {
        public abstract IEnumerator OnLoadScene(object sender, ISceneGameSystem system);
    }
}