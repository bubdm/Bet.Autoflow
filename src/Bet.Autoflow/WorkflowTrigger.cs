namespace Bet.Autoflow
{
    /// <summary>
    /// A generic trigger class.
    /// </summary>
    public class WorkflowTrigger<T> : IWorkflowTrigger<T>
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="Autoflow.WorkflowTrigger{T}"/>
        /// </summary>
        /// <param name="id"></param>
        public WorkflowTrigger(T id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value> The Identifier. </value>
        public T Id { get; set; }

        /// <summary>
        /// Gets or sets the description of this trigger. This provides additional information about the trigger.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display name. Use this for example for buttons.
        /// </summary>
        /// <value> The display name. </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Autoflow.WorkflowTrigger{T}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="Autoflow.WorkflowTrigger{T}"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Trigger: Id={0}, DisplayName={1}]", Id, DisplayName);
        }
    }
}
