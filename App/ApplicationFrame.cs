using System;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A base application frame.</para>
    /// </summary>
    public abstract class ApplicationFrame : Element, IApplicationFrame
    {
        ///<inheritdoc cref="IApplicationFrame.clientLayer"/> 
        public IClientLayer clientLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.databaseLayer"/> 
        public IDatabaseLayer databaseLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.repositoryLayer"/> 
        public IRepositoryLayer repositoryLayer { get; private set; }

        ///<inheritdoc cref="IApplicationFrame.interactorLayer"/> 
        public IInteractorLayer interactorLayer { get; private set; }

        #region OnCreate

        /// <summary>
        ///     <para>Creates the layers by their types.</para>
        /// </summary>
        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            this.clientLayer = this.CreateElement<IClientLayer>(this.GetClientLayerType());
            this.databaseLayer = this.CreateElement<IDatabaseLayer>(this.GetDatabaseLayerType());
            this.repositoryLayer = this.CreateElement<IRepositoryLayer>(this.GetRepositoryLayerType());
            this.interactorLayer = this.CreateElement<IInteractorLayer>(this.GetInteractorLayerType());
            this.OnCreate(this, context);
        }

        protected virtual void OnCreate(ApplicationFrame _, IElementContext context)
        {
        }

        /// <summary>
        ///     <para>Returns a type of client layer whose will created.</para>
        /// </summary>
        protected virtual Type GetClientLayerType()
        {
            return typeof(ClientLayer);
        }

        /// <summary>
        ///     <para>Returns a type of database layer whose will created.</para>
        /// </summary>
        protected virtual Type GetDatabaseLayerType()
        {
            return typeof(DatabaseLayer);
        }

        /// <summary>
        ///     <para>Returns a type of repository layer whose will created.</para>
        /// </summary>
        protected virtual Type GetRepositoryLayerType()
        {
            return typeof(RepositoryLayer);
        }

        /// <summary>
        ///     <para>Returns a type of interactor layer whose will created.</para>
        /// </summary>
        protected virtual Type GetInteractorLayerType()
        {
            return typeof(InteractorLayer);
        }

        #endregion
    }
}