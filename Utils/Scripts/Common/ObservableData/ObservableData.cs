namespace OregoFramework.Util
{
    public class ObservableData<T>
    {
        public delegate void ValueChangedHandler(object sender, ObservableData<T> self, T value);

        public event ValueChangedHandler OnChangedEvent;

        private T value;

        public ObservableData()
        {
        }

        public ObservableData(T value) : this()
        {
            this.value = value;
        }

        public T GetValue()
        {
            return this.value;
        }
        
        public void SetValue(object sender, T value)
        {
            this.value = value;
            this.OnChangedEvent?.Invoke(sender, this, value);
        }
    }
}