namespace Project.StateMachine
{
    public interface IState
    {
        /// <summary>
        /// Gets called when first entering the state
        /// </summary>
        public void Enter();

        /// <summary>
        /// Gets  called when exiting the state
        /// </summary>
        public void Exit();

        /// <summary>
        /// Gets called every Unity Update
        /// </summary>
        public void StateUpdate();

        /// <summary>
        /// Check for transitions, gets called every StateUpdate
        /// </summary>
        public void CheckTransitions();
    }
}
