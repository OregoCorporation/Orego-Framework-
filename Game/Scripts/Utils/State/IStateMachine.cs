using System.Collections.Generic;

namespace OregoFramework.Util
{
    public interface IStateMachine : IStateController, IState
    {
        T GetState<T>() where T : IState;

        IEnumerable<T> GetStates<T>() where T : IState;
    }

    public partial class Extensions
    {
        public static void ChangeState<TState>(
            this IStateMachine stateMachine,
            IStateTransition transition = null
        ) where TState : IState
        {
            var nextState = stateMachine.GetState<TState>();
            stateMachine.SetCurrentState(nextState, transition);
        }
    }
}