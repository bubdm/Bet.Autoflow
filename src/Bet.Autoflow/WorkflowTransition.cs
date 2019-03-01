namespace Bet.Autoflow
{
    /// <summary>
    /// The state transition in the workflow engine.
    /// </summary>
    public class WorkflowTransition<T, TContext>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkflowTransition(){}

        /// <summary>
        /// Construct a transition.
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        /// <param name="triggeredBy"></param>
        /// <param name="isReentry"></param>
        /// <param name="guardCondition"></param>
        /// <returns></returns>
        public WorkflowTransition(WorkflowState<T, TContext> fromState,
                                  WorkflowState<T, TContext> toState,
                                  WorkflowTrigger<T> triggeredBy,
                                  bool isReentry = false,
                                  IWorkflowGuard<T,TContext> guardCondition=null)
        {
            FromState = fromState;
            ToState = toState;
            TriggeredBy = triggeredBy;
            IsReentry = isReentry;
            GuardCondition = guardCondition;
        }

        /// <summary>
        /// The state transitioned from.
        /// </summary>
        public WorkflowState<T, TContext> FromState { get; set; }

        /// <summary>
        /// The state transitioned to.
        /// </summary>
        public WorkflowState<T, TContext> ToState { get; set; }

        /// <summary>
        /// The trigger that caused the transition.
        /// </summary>
        public WorkflowTrigger<T> TriggeredBy { get; set; }

        /// <summary>
        /// True if the transition is a re-entry, i.e. the identity transition.
        /// </summary>
        public bool IsReentry { get; set; } //= FromState.Equals(ToState);

        /// <summary>
        /// Provides with functionality to inject custom Guards conditions
        /// </summary>
        public IWorkflowGuard<T, TContext> GuardCondition { get; set; }
    }
}
