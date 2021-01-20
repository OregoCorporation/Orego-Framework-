namespace OregoFramework.Module
{
    /// <summary>
    ///     <para>Common analytics parameter.</para>
    /// </summary>
    public sealed class AnalyticsParam
    {
        public string key { get; set; }

        public string value { get; set; }

        public AnalyticsParam(string key, object value)
        {
            this.key = key;
            this.value = value.ToString();
        }
    }
}