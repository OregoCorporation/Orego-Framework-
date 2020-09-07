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

        #region Lifecycle

        private LoggerConfig config;

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
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

        protected abstract void Log(LogArgs logArgs);

        protected T GetConfig<T>() where T : LoggerConfig
        {
            return (T) this.config;
        }
    }
}