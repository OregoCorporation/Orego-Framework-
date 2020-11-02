namespace OregoFramework.Unit
{
    /// <summary>
    ///     <para>Common analytics parameter.</para>
    /// </summary>
    public sealed class AnalyticsParameter
    {
        public string key { get; set; }

        public string value { get; set; }

        public AnalyticsParameter(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}