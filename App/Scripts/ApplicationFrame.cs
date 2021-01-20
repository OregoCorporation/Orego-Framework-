using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///    <para>Architectural application frame with abstract layers.</para>
    /// </summary>
    public interface IApplicationFrame : IRootElement
    {
        /// <summary>
        ///     <para>Works with network.</para>
        /// </summary>
        IClientLayer ClientLayer { get; }
        
        /// <summary>
        ///     <para>Works with local storage.</para>
        /// </summary>
        IDatabaseLayer DatabaseLayer { get; }
        
        /// <summary>
        ///     <para>Works with data logic.</para>
        /// </summary>
        IRepositoryLayer RepositoryLayer { get; }

        /// <summary>
        ///     <para>Works with business logic.</para>
        /// </summary>
        IInteractorLayer InteractorLayer { get; }
    }
    
    /// <summary>
    ///     <para>A base application frame.</para>
    /// </summary>
    public abstract class ApplicationFrame : Element, IApplicationFrame
    {
        ///<inheritdoc cref="IApplicationFrame.ClientLayer"/> 
        public IClientLayer ClientLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.DatabaseLayer"/> 
        public IDatabaseLayer DatabaseLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.RepositoryLayer"/> 
        public IRepositoryLayer RepositoryLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.InteractorLayer"/> 
        public IInteractorLayer InteractorLayer { get; private set; }

        #region OnCreate

        /// <summary>
        ///     <para>Creates the layer instances by their types.</para>
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            this.ClientLayer = this.CreateClientLayer();
            this.DatabaseLayer = this.CreateDatabaseLayer();
            this.RepositoryLayer = this.CreateRepositoryLayer();
            this.InteractorLayer = this.CreateInteractorLayer();
        }
        
        /// <summary>
        ///     <para>Creates a client layer instance.</para>
        /// </summary>
        protected virtual IClientLayer CreateClientLayer()
        {
            return this.CreateElement<IClientLayer>(typeof(ClientLayer));
        }
        
        /// <summary>
        ///     <para>Creates a database layer instance.</para>
        /// </summary>
        protected virtual IDatabaseLayer CreateDatabaseLayer()
        {
            return this.CreateElement<IDatabaseLayer>(typeof(DatabaseLayer));
        }

        /// <summary>
        ///     <para>Creates a repository layer instance.</para>
        /// </summary>
        protected virtual IRepositoryLayer CreateRepositoryLayer()
        {
            return this.CreateElement<IRepositoryLayer>(typeof(RepositoryLayer));
        }
        
        /// <summary>
        ///     <para>Creates a interactor layer instance.</para>
        /// </summary>
        protected virtual IInteractorLayer CreateInteractorLayer()
        {
            return this.CreateElement<IInteractorLayer>(typeof(InteractorLayer));
        }

        #endregion
    }
}