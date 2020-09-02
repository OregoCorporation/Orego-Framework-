using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    /// <para>Base interface layer of domain controllers to work with business logic.</para>
    /// </summary>
    public interface IInteractorLayer : IDomainElement
    {
        /// <summary>
        ///     <para>Gets required interactor.</para>
        /// </summary>
        /// <typeparam name="T">Required type of interactor.</typeparam>
        /// <returns>Required interactor reference.</returns>
        T GetInteractor<T>() where T : IInteractor;

        /// <summary>
        ///     <para>Gets required interactors.</para>
        /// </summary>
        /// <typeparam name="T">Required type of interactor.</typeparam>
        /// <returns>Required interactor set.</returns>
        IEnumerable<T> GetInteractors<T>() where T : IInteractor;
    }
}