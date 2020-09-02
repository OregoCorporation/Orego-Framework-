using System;
using System.Collections;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class Content : ElementLayer<IContentSection>, IRootElement
    {
        #region Event

        public static event Action OnLoadedEvent;

        #endregion

        private static Content instance;

        public override void OnCreate(IElementContext context)
        {
            base.OnCreate(context);
            instance = this;
        }

        public static IEnumerator LoadResources()
        {
            yield return instance.LoadResourcesInternal();
            OnLoadedEvent?.Invoke();
        }

        protected virtual IEnumerator LoadResourcesInternal()
        {
            var elements = this.GetElements<IContentSection>();
            foreach (var section in elements)
            {
                yield return section.LoadResources();
            }
        }

        public static T GetSection<T>() where T : IContentSection
        {
            return instance.GetSectionInternal<T>();
        }

        protected T GetSectionInternal<T>() where T : IContentSection
        {
            return instance.GetElement<T>();
        }
    }
}