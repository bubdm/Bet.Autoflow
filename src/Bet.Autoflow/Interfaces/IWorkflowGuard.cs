namespace Bet.Autoflow
{
    /// <summary>
    /// The workflow guard condition. It validates the state change.
    /// </summary>
    public interface IWorkflowGuard<T,TContext>
    {
        /// <summary>
        /// Determines whether the specified state change is allowed.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <returns></returns>
        bool IsAllowed(IWorkflow<T,TContext> workflow);
    }
}
