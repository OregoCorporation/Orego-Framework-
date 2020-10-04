namespace OregoFramework.App
{
    public interface IBaseClient : IClient
    {
        /// <summary>
        ///     <para>Sends requests on low level.</para>
        /// </summary>
        IRequestChannel channel { get; }
    }
}