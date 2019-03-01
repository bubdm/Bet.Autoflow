using System.Collections.Generic;

namespace Bet.Autoflow
{
    /// <summary>
    /// The workflow interface to be used with the actions to be executed.
    /// </summary>
    public interface IWorkflow<T, TContext>
    {
        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
        /// <param name="trigger">The trigger to that force the move to a new state.</param>
        void ChangeState(T trigger);

        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
        /// <param name="trigger">The trigger to that force the move to a new state.</param>
        void ChangeState(WorkflowTrigger<T> trigger);

        /// <summary>
        /// Gets the permitted triggers based on the current state and the allowed triggers for this state.
        /// </summary>
        /// <value>The permitted triggers.</value>
        IEnumerable<WorkflowTrigger<T>> PermittedTriggers { get; }

        /// <summary>Gets or sets the state of the workflow.
        /// </summary>
        /// <value>The state of the workflow.</value>
        WorkflowState<T, TContext> CurrentState { get; }

        /// <summary>
        /// Determines whether this workflow can change its state based on the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        bool CanChangeState(WorkflowTrigger<T> trigger);

        /// <summary>
        /// Represents the workflow in DotGraph format.
        /// </summary>
        /// <returns></returns>
        string ToDotGraph();

        /// <summary>
        /// Provides the workflow context.
        /// </summary>
        TContext Context { get; }
    }
}
