using System.Threading.Tasks;

namespace Bet.Autoflow
{
    /// <summary>
    /// An Action to perform by the state machine
    /// </summary>
    /// <typeparam name="T">The type of the action.</typeparam>
    /// <typeparam name="TContext">The context of the execution.</typeparam>
    public interface IAction<T, TContext>
    {
        /// <summary>
        /// Execute sync an action on entry or on exit of the triggered workflow transition.
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="transition"></param>
        void Execute(IWorkflow<T,TContext> workflow, WorkflowTransition<T,TContext> transition);

        /// <summary>
        /// Execute async an action on entry or on exit of the triggered workflow transition.
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="transition"></param>
        Task ExecuteAsync(IWorkflow<T, TContext> workflow, WorkflowTransition<T, TContext> transition);
    }
}
