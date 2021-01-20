using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class BaseLogger : Logger
    {
        #region Log

        protected sealed override void LogInternal(LogArgs args)
        {
            if (this.CanLog(args))
            {
                var message = args.message;
                Debug.Log(message);
            }
        }

        protected virtual bool CanLog(LogArgs logArgs)
        {
            var loggerConfig = this.GetConfig<LoggerConfig>();
            if (loggerConfig.level != logArgs.level)
            {
                return false;
            }

            if (loggerConfig.profile != logArgs.profile)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}