namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A domain controller interface.</para>
    /// </summary>
    public interface IInteractor : IDomainElement
    {
    }
    
    /// <summary>
    ///     <para>An interactor class.</para>
    /// </summary>
    public abstract class Interactor : DomainElement, IInteractor
    {
    }
}