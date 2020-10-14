using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IEventPipe
    {
        IEvent PushEvent(IEvent inputEvent);
        
        List<IEventHandler> handlerSequence { get; }
    }
}