using Gullis;
using OregoFramework.App;

namespace OregoFramework.Module
{
    public interface IUIGameplayElement
    {
        void AttachContext(IGameContext context);
        
        void OnGamePrepared();

        void OnGameReady();

        void OnGameStarted();

        void OnGamePaused();

        void OnGameResumed();

        void OnGameFinished();

        void DetachContext();
    }
    
    public sealed class UIGameplayElement : UIElement, IUIGameplayElement
    {
        public void AttachContext(IGameContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnGamePrepared()
        {
            throw new System.NotImplementedException();
        }

        public void OnGameReady()
        {
            throw new System.NotImplementedException();
        }

        public void OnGameStarted()
        {
            throw new System.NotImplementedException();
        }

        public void OnGamePaused()
        {
            throw new System.NotImplementedException();
        }

        public void OnGameResumed()
        {
            throw new System.NotImplementedException();
        }

        public void OnGameFinished()
        {
            throw new System.NotImplementedException();
        }

        public void DetachContext()
        {
            throw new System.NotImplementedException();
        }
    }
}