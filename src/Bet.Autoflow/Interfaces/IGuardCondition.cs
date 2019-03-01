namespace Bet.Autoflow
{
    /// <summary>
    /// The Conditional guards for the execution of the next state.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public interface IGuardCondition<T, TContext>
    {
        /// <summary>
        /// Determines whether the specified state change is allowed.
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        bool IsAllowed(IWorkflow<T, TContext> workflow);
    }
}
