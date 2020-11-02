using System.Collections.Generic;
using Elementary;

namespace OregoFramework.Unit
{
    /// <summary>
    ///     <para>Report analytics events of one scope.</para>
    /// </summary>
    public abstract class AnalyticsController : Element
    {
        protected void LogEvent(string eventKey, IEnumerable<AnalyticsParameter> parameters = null)
        {
            AnalyticsSystem.LogEvent(eventKey, parameters);
        }
    }
}