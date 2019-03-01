using System.Collections.Generic;

namespace Bet.Autoflow
{
	/// <summary>
	/// The state collection.
	/// </summary>
	public class WorkflowStateCollection<T, TContext> : Dictionary<T, WorkflowState<T, TContext>>
	{
        /// <summary>
        /// The item to add to the current collection.
        /// Add the specified state.
        /// </summary>
        /// <param name="state">State.</param>
        public void Add(WorkflowState<T, TContext> state)
		{
			Add(state.Id, state);
		}
	}
}
