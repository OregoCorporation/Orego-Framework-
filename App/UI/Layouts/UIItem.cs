namespace OregoFramework.App
{
    public abstract class UIItem<T> : UIElement
    {
        public T currentItem { get; protected set; }

        public virtual void SetItem(T item)
        {
            this.currentItem = item;
            this.UpdateState();
        }

        public virtual void UpdateState()
        {
        }
    }
}