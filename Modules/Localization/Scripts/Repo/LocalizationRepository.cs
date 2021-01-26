using System;
using System.Collections;
using OregoFramework.App;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class LocalizationRepository : Repository
    {
        #region Event

        public event Action<LocalizationState> OnStateReadyEvent;

        #endregion

        protected IUpdateDataHandler dataHandler { get; private set; }

        protected LocalizationStateDao dao { get; private set; }

        #region Lifecycle

        protected override void OnCreate()
        {
            base.OnCreate();
            this.dataHandler = this.CreateElement<LocalizationDataHandler>();
        }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            var database = this.GetDatabase<IPrefsDatabase>();
            this.dao = database.GetDao<LocalizationStateDao>();
        }

        protected IEnumerator BeginSession()
        {
            yield return this.dataHandler.CheckForUpdates();
            var state = this.dao.GetState();
            this.OnStateReadyEvent?.Invoke(state);
        }

        #endregion

        public void UpdateLanguage(SystemLanguage language)
        {
            var state = new LocalizationState
            {
                language = language
            };
            this.dao.SetState(state);
        }
    }
}