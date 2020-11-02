using Elementary;
using UnityEngine;

namespace OregoFramework.Unit
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

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            instance = this;
            var asset = Resources.Load<LoggerConfig>(CONFIG_NAME);
            this.config = ScriptableObject.Instantiate(asset);
            this.OnCreate(this);
        }

        protected virtual void OnCreate(Logger _)
        {
        }

        protected override void OnDispose(Element _)
        {
            ScriptableObject.Destroy(this.config);
            this.OnDispose(this);
        }

        protected virtual void OnDispose(Logger _)
        {
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