namespace OregoFramework.App
{
    public abstract class StatefulPrefsDao<T> : EncryptablePrefsDao<T>
    {
        public T GetState()
        {
            return this.GetEntity(this.Key);
        }

        public bool HasState()
        {
            return this.HasEntity(this.Key);
        }

        public void SetState(T state)
        {
            this.SetEntity(this.Key, state);
        }
    }
}