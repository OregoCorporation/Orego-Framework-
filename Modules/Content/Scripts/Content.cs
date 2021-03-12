using System.Collections;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public interface IContent : IElement
    {
        IEnumerator PreloadResources();
    }
    
    public abstract class Content : Element, IContent
    {
        IEnumerator IContent.PreloadResources()
        {
            yield return this.OnPreloadResources();
        }

        protected virtual IEnumerator OnPreloadResources()
        {
            yield break;
        }

        protected virtual IEnumerator LoadAsset<T>(string path, Reference<T> result) where T : Object
        {
            var request = Resources.LoadAsync<T>(path);
            yield return request;
            result.value = request.asset.As<T>();
        }

        protected virtual T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        protected virtual IEnumerable<T> LoadAssets<T>(string directory) where T : Object
        {
            return Resources.LoadAll<T>(directory);
        } 
    }
}