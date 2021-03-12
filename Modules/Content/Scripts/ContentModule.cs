using System;
using System.Collections;
using Elementary;

namespace OregoFramework.Module
{
    public abstract class ContentModule : ElementLayer<IContent>, IRootElement
    {
        #region Event

        public static event Action OnPreloadedEvent;

        #endregion

        private static ContentModule instance;

        protected sealed override void OnCreate()
        {
            base.OnCreate();
            instance = this;
        }
        
        public static IEnumerator PreloadResources()
        {
            yield return instance.PreloadResourcesInternal();
            OnPreloadedEvent?.Invoke();
        }

        protected IEnumerator PreloadResourcesInternal()
        {
            var contentSet = this.GetElements<IContent>();
            foreach (var content in contentSet)
            {
                yield return content.PreloadResources();
            }
        }

        public static T GetContent<T>() where T : IContent
        {
            return instance.GetContentInternal<T>();
        }

        protected T GetContentInternal<T>() where T : IContent
        {
            return instance.GetElement<T>();
        }
    }
}