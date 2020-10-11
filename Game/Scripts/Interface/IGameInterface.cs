using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IGameInterface
    {
        IGameContext currentGameContext { get; }

        void RegisterForm(IGameInterfaceForm form);

        void UnregisterForm(IGameInterfaceForm form);

        T GetForm<T>() where T : IGameInterfaceForm;

        IEnumerable<T> GetForms<T>() where T : IGameInterfaceForm;
    }
}