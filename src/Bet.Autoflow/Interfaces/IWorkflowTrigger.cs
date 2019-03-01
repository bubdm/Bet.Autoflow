namespace Bet.Autoflow
{
    /// <summary>
    /// Provides with the signature for the triggers to be stored based on their type definition
    /// </summary>
    /// <typeparam name="T">Type of the Trigger: string, enum</typeparam>
    public interface IWorkflowTrigger<T> : ISpecializedBy<T>
    {
        /// <summary>
        /// Gets or sets the description of this trigger instance.
        /// This should provide additional information about the trigger.
        /// </summary>
        /// <value> The description of the trigger</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string DisplayName { get; set; }
    }
}
