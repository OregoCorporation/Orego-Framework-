using System;
using Elementary;

namespace OregoFramework.Module
{
    /// <summary>
    ///     <para>Reports analytics events of single scope.</para>
    /// </summary>
    public abstract class AnalyticsHandler : Element, IAnalyticsHandler
    {
        #region Events

        public event Action<string, AnalyticsParam[]> OnLogEvent;

        #endregion

        protected internal void LogEvent(string eventKey, params AnalyticsParam[] parameters)
        {
            this.OnLogEvent?.Invoke(eventKey, parameters);
        }
    }

    public abstract class SingletonAnalyticsHandler<T> : AnalyticsHandler where T : AnalyticsHandler
    {
        protected static T instance;

        protected sealed override void OnCreate()
        {
            base.OnCreate();
            instance = this as T;
        }
        
        protected static void LogEventStatic(string eventKey, params AnalyticsParam[] parameters)
        {
            instance.LogEvent(eventKey, parameters);
        }
    }
}