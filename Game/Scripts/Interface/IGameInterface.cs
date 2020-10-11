using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IGameInterface
    {
        IGameContext currentGameContext { get; }

        void AddForm(IGameInterfaceForm form);

        void RemoveForm(IGameInterfaceForm form);

        T GetForm<T>() where T : IGameInterfaceForm;

        IEnumerable<T> GetForms<T>() where T : IGameInterfaceForm;
    }
}