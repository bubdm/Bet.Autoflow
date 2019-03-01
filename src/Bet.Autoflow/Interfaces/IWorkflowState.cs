namespace Bet.Autoflow
{
	/// <summary>
	/// A state in the workflow sequence that is to be executed.
	/// </summary>
	public interface IWorkflowState<T, TContext>
	{
		/// <summary>
		/// Gets or sets the description of this state. This provides additional information about the state.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>The display name.</value>
		string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the super state.
		/// </summary>
		/// <value>The state of the super.</value>
		WorkflowState<T, TContext> SuperState { get; set; }

		/// <summary>
		/// Gets or sets the entry action that will be executed when the state is entered.
		/// </summary>
		/// <value>The entry action for the state.</value>
		IAction<T, TContext> EntryAction { get; set; }

		/// <summary>
		/// Gets or sets the exit action that will be executed when the state is been exited.
		/// </summary>
		/// <value>The exit action for the state.</value>
		IAction<T, TContext> ExitAction { get; set; }

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		/// <value> The Identifier of this state.</value>
		T Id { get; set; }
	}
}
