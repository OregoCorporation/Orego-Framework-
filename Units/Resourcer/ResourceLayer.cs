using System.Collections.Generic;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class ResourceLayer : ElementLayer<IResource>, IRootElement
    {
        private static ResourceLayer instance;

        protected override void OnCreate(ElementLayer<IResource> _, IElementContext context)
        {
            instance = this;
            this.OnCreate(this, context);
        }

        protected virtual void OnCreate(ResourceLayer _, IElementContext context)
        {
        }

        public static IEnumerable<T> GetResources<T>() where T : IResource
        {
            return instance.GetResourcesInternal<T>();
        }

        private IEnumerable<T> GetResourcesInternal<T>() where T : IResource
        {
            return this.GetElements<T>();
        }

        public static T GetResource<T>() where T : IResource
        {
            return instance.GetResourceInternal<T>();
        }

        private T GetResourceInternal<T>() where T : IResource
        {
            return this.GetElement<T>();
        }
    }
}