using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Base abstract implementation of client interface.</para>
    /// </summary>
    /// <typeparam name="T">Requred caller type.</typeparam>
    public abstract class Client<T> : ElementLayer<T>, IClient where T : ICaller
    {
        public TCaller GetCaller<TCaller>() where TCaller : ICaller
        {
            return this.GetElement<TCaller>();
        }

        public IEnumerable<TCaller> GetCallers<TCaller>() where TCaller : ICaller
        {
            return this.GetElements<TCaller>();
        }
    }
}