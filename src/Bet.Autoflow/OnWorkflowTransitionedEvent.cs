using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bet.Autoflow
{
    /// <summary>
    /// The workflow transitioned event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    internal class OnWorkflowTransitionedEvent<T, TContext>
    {
        private event Action<WorkflowTransition<T, TContext>> _onTransitioned;
        private readonly List<Func<WorkflowTransition<T, TContext>, Task>> _onTransitionedAsync = new List<Func<WorkflowTransition<T, TContext>, Task>>();

        public void Invoke(WorkflowTransition<T, TContext> transition)
        {
            if (_onTransitionedAsync.Count != 0)
            {
                throw new InvalidOperationException(
                    "Cannot execute asynchronous action specified as OnTransitioned callback. " +
                    "Use asynchronous version of Fire [FireAsync]");
            }

            _onTransitioned?.Invoke(transition);
        }

        public async Task InvokeAsync(WorkflowTransition<T, TContext> transition)
        {
            _onTransitioned?.Invoke(transition);

            foreach (var callback in _onTransitionedAsync)
            {
                await callback(transition);
            }
        }

        public void Register(Action<WorkflowTransition<T, TContext>> action)
        {
            _onTransitioned += action;
        }

        public void Register(Func<WorkflowTransition<T, TContext>, Task> action)
        {
            _onTransitionedAsync.Add(action);
        }
    }
}
