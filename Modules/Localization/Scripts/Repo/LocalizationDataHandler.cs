using System.Collections;
using OregoFramework.App;
using OregoFramework.Util;

namespace OregoFramework.Module
{
    public abstract class LocalizationDataHandler : UpdateDataHandler
    {
        protected LocalizationContent content { get; private set; }

        protected LocalizationStateDao dao { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            var database = this.GetDatabase<IPrefsDatabase>();
            this.dao = database.GetDao<LocalizationStateDao>();
            this.content = ContentModule.GetContent<LocalizationContent>();
        }

        public override IEnumerator CheckForUpdates(Reference<bool> isUpdated = null)
        {
            if (this.dao.HasState())
            {
                yield break;
            }
            
            var language = UnityEngine.Application.systemLanguage;
            var config = this.content.config;
            if (!config.IsLanguageSupported(language))
            {
                language = config.defaultLanguage;
            }

            var state = new LocalizationState
            {
                language = language
            };
            this.dao.SetState(state);
        }
    }
}