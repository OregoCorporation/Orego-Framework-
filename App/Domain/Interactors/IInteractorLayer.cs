using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An interactor layer interface.</para>
    ///     <para>Keeps dictionary of unique interactors <see cref="IInteractor"/>.</para>
    /// </summary>
    public interface IInteractorLayer : IDomainElement
    {
        /// <summary>
        ///     <para>Returns a required interactor of "T" type.</para>
        /// </summary>
        T GetInteractor<T>() where T : IInteractor;

        /// <summary>
        ///     <para>Returns a required interactors of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetInteractors<T>() where T : IInteractor;
    }
}