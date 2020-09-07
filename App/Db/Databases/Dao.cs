using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Base abstract data access object implementation.</para>
    /// </summary>
    public abstract class Dao<T> : Element, IDao where T : IDatabase
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected T database { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            var databaseLayer = this.applicationFrame.databaseLayer;
            this.database = databaseLayer.GetDatabase<T>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(Dao<T> _)
        {
        }
    }
}