namespace OregoFramework.Util
{
    public abstract class BaseMonoStateMachine : MonoStateMachine, IDelegableState
    {
        protected object parent { get; private set; }

        public void OnProvideParent(object parent)
        {
            this.parent = parent;
            this.OnParentProvided();
        }

        protected virtual void OnParentProvided()
        {
            var states = this.GetStates<IDelegableState>();
            foreach (var state in states)
            {
                state.OnProvideParent(this.parent);
            }
        }
        
        public void ChangeState<TState>(IStateTransition transition = null) where TState : IState
        {
            var nextState = this.GetState<TState>();
            this.SetCurrentState(nextState, transition);
        }
    }
}