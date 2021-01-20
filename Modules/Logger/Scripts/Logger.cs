using Elementary;
using UnityEngine;

namespace OregoFramework.Module
{
    /// <summary>
    ///     <para>Abstract logger.</para>
    /// </summary>
    public abstract class Logger : Element, IRootElement
    {
        #region Const

        public const string CONFIG_NAME = "LoggerConfig";

        #endregion

        protected static Logger instance;
        
        private LoggerConfig config;

        #region Lifecycle

        protected sealed override void OnCreate()
        {
            base.OnCreate();
            instance = this;
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.config = Resources.Load<LoggerConfig>(CONFIG_NAME);
        }

        #endregion

        public static void Log(LogArgs args)
        {
            instance.LogInternal(args);
        }

        protected abstract void LogInternal(LogArgs args);

        protected T GetConfig<T>() where T : LoggerConfig
        {
            return (T) this.config;
        }
    }
}