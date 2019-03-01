using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bet.Autoflow
{
    /// <summary>
    /// Combines multiple actions together. Each action will be executed,
    /// but if one fails, an exception will be thrown.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class AggregateAction<T, TContext> : IAction<T, TContext>
    {
        private readonly IEnumerable<IAction<T, TContext>> actions;

        /// <summary>
        /// Constructor for creating actions
        /// </summary>
        /// <param name="actions"></param>
        public AggregateAction(IEnumerable<IAction<T,TContext>> actions)
        {
            Guard.ArgumentIsNotNull(actions, nameof(actions));
            this.actions = actions;
        }

        /// <summary>
        ///  Executes workflow combined actions
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="transition"></param>
        public void Execute(IWorkflow<T, TContext> workflow, WorkflowTransition<T, TContext> transition)
        {
            foreach (var action in actions)
            {
                try
                {
                    action.Execute(workflow, transition);
                }
                catch (Exception ex)
                {
                    throw new WorkflowActionException($"Unable to execute action {action.GetType()}",ex);
                }
            }
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="transition"></param>
        public async Task ExecuteAsync(IWorkflow<T, TContext> workflow, WorkflowTransition<T, TContext> transition)
        {
            //TODO: implement async
            throw new NotImplementedException();
        }
    }
}
