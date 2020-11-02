using System.Collections.Generic;
using Elementary;

namespace OregoFramework.Unit
{
    /// <summary>
    ///     <para>Abstract facade for analytics reporting.</para>
    /// </summary>
    public abstract class AnalyticsSystem : Element, IRootElement
    {
        protected static AnalyticsSystem instance;

        protected IEnumerable<AnalyticsController> controllers { get; private set; }

        protected override void OnCreate(Element _, IElementContext context)
        {
            instance = this;
            this.controllers = this.CreateElements<AnalyticsController>();
        }

        public static void LogEvent(
            string eventKey,
            IEnumerable<AnalyticsParameter> parameters = null
        )
        {
            instance.LogEventInternal(eventKey, parameters);
        }

        protected abstract void LogEventInternal(
            string eventKey,
            IEnumerable<AnalyticsParameter> parameters = null
        );
    }
}