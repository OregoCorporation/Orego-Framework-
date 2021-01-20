using System;
using Elementary;

namespace OregoFramework.Module
{
    public interface IAnalyticsHandler : IElement
    {
        event Action<string, AnalyticsParam[]> OnLogEvent;
    }
}