using OregoFramework.App;

namespace OregoFramework.Module
{
    public interface IUIGameController
    {
        void OnRegistered(UIGameScreen screen);

        void OnGamePrepared(object sender);

        void OnGameReady(object sender);

        void OnGameStarted(object sender);

        void OnGamePaused(object sender);

        void OnGameResumed(object sender);

        void OnGameFinished(object sender);

        void OnUnregistered();
    }

    public abstract class UIGameController : UIElement, IUIGameController
    {
        protected UIGameScreen Screen;

        void IUIGameController.OnRegistered(UIGameScreen screen)
        {
            this.Screen = screen;
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        void IUIGameController.OnGamePrepared(object sender)
        {
            this.OnGamePrepared(sender);
        }

        protected virtual void OnGamePrepared(object sender)
        {
        }

        void IUIGameController.OnGameReady(object sender)
        {
            this.OnGameReady(sender);
        }

        protected virtual void OnGameReady(object sender)
        {
        }

        void IUIGameController.OnGameStarted(object sender)
        {
            this.OnGameStarted(sender);
        }

        protected virtual void OnGameStarted(object sender)
        {
        }

        void IUIGameController.OnGamePaused(object sender)
        {
            this.OnGamePaused(sender);
        }

        protected virtual void OnGamePaused(object sender)
        {
        }

        void IUIGameController.OnGameResumed(object sender)
        {
            this.OnGameResumed(sender);
        }

        protected virtual void OnGameResumed(object sender)
        {
        }

        void IUIGameController.OnGameFinished(object sender)
        {
            this.OnGameFinished(sender);
        }

        protected virtual void OnGameFinished(object sender)
        {
        }

        void IUIGameController.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }

        protected T GetScreen<T>() where T : UIGameScreen
        {
            return (T) this.Screen;
        }

        protected T GetContext<T>()
        {
            return (T) this.Screen.AttachedContext;
        }
    }
}