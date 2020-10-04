using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Data access object class.</para>
    /// </summary>
    public abstract class Dao : Element, IDao
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(Dao _)
        {
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
        protected T database { get; private set; }

        protected sealed override void OnPrepare(Dao _)
        {
            this.database = this.applicationFrame.databaseLayer.GetDatabase<T>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(Dao<T> _)
        {
        }
    }
}