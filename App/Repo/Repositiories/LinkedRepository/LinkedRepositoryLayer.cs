using System.Collections;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Keeps repositories that are dependent on each other.</para>
    ///     <para>Updates user data entities to each other when a user session starts.</para>
    /// </summary>
    public abstract class LinkedRepositoryLayer : SessionRepositoryLayer
    {
        /// <inheritdoc cref="IRepositoryLinker"/>
        public IRepositoryLinker linker { get; private set; }

        protected sealed override void OnCreate(
            ElementLayer<IRepository> _,
            IElementContext context
        )
        {
            this.linker = this.CreateLinker();
            this.OnCreate(this);
        }

        /// <summary>
        ///     <para>Creates a new instance of repository linker.</para>
        /// </summary>
        /// <returns>A new instance.</returns>
        protected abstract IRepositoryLinker CreateLinker();

        protected virtual void OnCreate(LinkedRepositoryLayer _)
        {
        }

        protected sealed override IEnumerator OnAfterBeginSession()
        {
            yield return this.linker.LinkData();
            yield return this.OnAfterBeginSession(this);
        }

        protected virtual IEnumerator OnAfterBeginSession(LinkedRepositoryLayer _)
        {
            yield break;
        }
    }
}