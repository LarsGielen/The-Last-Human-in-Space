
namespace Project.Entity
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;
            currentState.Enter();
        }

        public void StateUpdate()
        {
            currentState.StateUpdate();
            currentState.CheckTransitions();
        }
    }
}
