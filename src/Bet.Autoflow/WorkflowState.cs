namespace Bet.Autoflow
{
	/// <summary>
	/// The state in the workflow engine.
	/// </summary>
	public class WorkflowState<T, TContext> : IWorkflowState<T, TContext>, ISpecializedBy<T>
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Autoflow.WorkflowState{T,TContext}"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="onEntryStateAction"></param>
        /// <param name="onExitStateAction"></param>
        /// <returns></returns>
        public WorkflowState(
            T id,
            IAction<T, TContext> onEntryStateAction = null,
            IAction<T, TContext> onExitStateAction = null)
		{
			Id = id;
			EntryAction = onEntryStateAction;
			ExitAction = onExitStateAction;
		}

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		/// <value> The Identifier of this state.</value>
		public T Id { get; set; }

		/// <summary>
		/// Gets or sets the description of this state. This provides additional information about the state.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value> The display name. </value>
		public string DisplayName { get; set; }

		/// <summary>Gets or sets the super state.</summary><value>The state of the super.</value>
		public WorkflowState<T, TContext> SuperState { get; set; }

		/// <summary>
		/// Gets or sets the entry action that will be executed when the state is entered.
		/// </summary>
		/// <value> The entry action. </value>
		public IAction<T, TContext> EntryAction { get; set; }

		/// <summary>
		/// Gets or sets the exit action that will be executed when the state is been exited.
		/// </summary>
		/// <value> The exit action.</value>
		public IAction<T, TContext> ExitAction { get; set; }

		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="Autoflow.WorkflowState{T,TContext}"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="Autoflow.WorkflowState{T,TContext}"/>.</returns>
		public override string ToString()
		{
			return $"[State: Id={Id}, DisplayName={DisplayName}]";
		}
	}
}
