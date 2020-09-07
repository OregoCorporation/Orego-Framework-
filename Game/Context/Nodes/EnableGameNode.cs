namespace OregoFramework.Game
{
    public abstract class EnableGameNode : GameNode
    {
        protected virtual void Awake()
        {
            this.enabled = false;
        }

        protected sealed override void OnStartGame(GameNode _, object sender)
        {
            this.enabled = true;
            this.OnStartGame(this, sender);
        }

        protected virtual void OnStartGame(EnableGameNode _, object sender)
        {
        }

        protected sealed override void OnPauseGame(GameNode _, object sender)
        {
            this.enabled = false;
            this.OnPauseGame(this, sender);
        }

        protected virtual void OnPauseGame(EnableGameNode _, object sender)
        {
        }

        protected sealed override void OnResumeGame(GameNode _, object sender)
        {
            this.enabled = true;
            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(EnableGameNode _, object sender)
        {
        }

        protected sealed override void OnFinishGame(GameNode _, object sender)
        {
            this.enabled = false;
            this.OnFinishGame(this, sender);
        }

        protected virtual void OnFinishGame(EnableGameNode _, object sender)
        {
        }
    }
}