namespace Bet.Autoflow
{
	/// <summary>
	/// Defines a workflow end state.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TContext"></typeparam>
	public class WorkflowEndState<T, TContext> : WorkflowState<T, TContext>
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Autoflow.WorkflowStartState{T, TContext}"/> class.
        /// </summary>
        /// <typeparam name="{T}"></typeparam>
        /// <typeparam name="{TContext}"></typeparam>
        /// <param name="id"></param>
        /// <param name="onEntryStateAction"></param>
        /// <param name="onExitStateAction"></param>
        public WorkflowEndState(
            T id,
            IAction<T, TContext> onEntryStateAction = null,
            IAction<T, TContext> onExitStateAction = null)
			: base(id, onEntryStateAction, onExitStateAction) { }
	}
}
