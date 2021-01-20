namespace OregoFramework.Util
{
    public sealed class Reference<T>
    {
        public T value { get; set; }

        public Reference()
        {
        }
        
        public Reference(T value)
        {
            this.value = value;
        }

        public bool HasValue()
        {
            return !Equals(this.value, default(T));
        }
    }
}