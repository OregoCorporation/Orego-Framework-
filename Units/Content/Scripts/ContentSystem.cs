using System;
using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class ContentSystem : ElementLayer<IContent>, IRootElement
    {
        #region Event

        public static event Action OnLoadedEvent;

        #endregion

        private static ContentSystem instance;

        protected sealed override void OnCreate(ElementLayer<IContent> _, IElementContext context)
        {
            instance = this;
            this.OnCreate(this);
        }

        protected virtual void OnCreate(ContentSystem _)
        {
        }

        public static IEnumerator LoadResources()
        {
            yield return instance.LoadResourcesInternal();
            OnLoadedEvent?.Invoke();
        }

        protected IEnumerator LoadResourcesInternal()
        {
            var contentSet = this.GetElements<IContent>();
            foreach (var content in contentSet)
            {
                yield return content.LoadResources();
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