using System.Collections.Generic;

namespace Bet.Autoflow
{
    /// <summary>
    /// The workflow transition collection to be executed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class WorkflowTransitionCollection<T,TContext> : List<WorkflowTransition<T,TContext>>
    {
    }
}
