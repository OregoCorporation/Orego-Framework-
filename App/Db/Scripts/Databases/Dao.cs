using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Data access object interface.</para>
    ///     <para>Each dao quieries of one case.</para>
    /// </summary>
    public interface IDao : IElement
    {
    }
    
    /// <summary>
    ///     <para>Data access object class.</para>
    /// </summary>
    public abstract class Dao : Element, IDao
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
        }
    }

    /// <summary>
    ///     <para>Data access object with database.</para>
    /// </summary>
    /// 
    /// <typeparam name="T">Type of parent database.</typeparam>
    public abstract class Dao<T> : Dao where T : IDatabase
    {
        /// <summary>
        ///     <para>A parent database reference.</para>
        /// </summary>
        protected T Database { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Database = this.Application.DatabaseLayer.GetDatabase<T>();
        }
    }
}