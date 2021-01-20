using System.Collections.Generic;
using Elementary;

namespace OregoFramework.Module
{
    /// <summary>
    ///     <para>Abstract facade for analytics reporting.</para>
    /// </summary>
    public abstract class AnalyticsModule : Element, IRootElement
    {
        protected IEnumerable<IAnalyticsHandler> hanlders { get; private set; }

        private static AnalyticsModule instance;
        
        #region Lifecycle

        protected override void OnCreate()
        {
            base.OnCreate();
            instance = this;
            this.hanlders = this.CreateElements<IAnalyticsHandler>();
        }

        protected override void OnReady()
        {
            base.OnReady();
            foreach (var hanlder in this.hanlders)
            {
                hanlder.OnLogEvent += this.OnLogEvent;
            }
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            foreach (var hanlder in this.hanlders)
            {
                hanlder.OnLogEvent -= this.OnLogEvent;
            }
        }

        #endregion

        #region HandlerCallbacks

        private void OnLogEvent(string eventKey, AnalyticsParam[] parameters)
        {
            this.LogEventInternal(eventKey, parameters);
        }

        #endregion

        public static void LogEvent(string eventKey, AnalyticsParam[] parameters)
        {
            instance.LogEventInternal(eventKey, parameters);
        }

        protected abstract void LogEventInternal(
            string eventKey,
            params AnalyticsParam[] parameters
        );
    }
}