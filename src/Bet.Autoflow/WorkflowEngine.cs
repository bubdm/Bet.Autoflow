using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;
using Stateless.Graph;

namespace Bet.Autoflow
{
    /// <summary>
    /// The workflow engine class.
    /// </summary>
    public class WorkflowEngine<T, TContext> : IWorkflow<T,TContext>
	{
		private readonly WorkflowDefinition<T, TContext> _workflowDefinition;
		private readonly StateMachine<WorkflowState<T, TContext>, WorkflowTrigger<T>> _stateMachine;

		readonly OnWorkflowTransitionedEvent<T, TContext> _onTransitionedEvent;
        private readonly TContext _workflowContext;

        /// <summary>
        /// Source, Destination, Triggered By Reentry
        /// </summary>
        public event Action<WorkflowState<T,TContext>, WorkflowState<T, TContext>,WorkflowTrigger<T>,bool> OnStateTransition;

        /// <summary>
        /// Allows for the unhandled triggers to be logged.
        /// </summary>
        public event Action<WorkflowState<T, TContext>,WorkflowTrigger<T>> OnTriggerUnhandled;

        #region IWorkflow Implementation
        /// <summary>
        /// Returns the context object that is used with conjunction with Guard
        /// </summary>
        public virtual TContext Context => _workflowContext;

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value> The state of the workflow.</value>
        public virtual WorkflowState<T, TContext> CurrentState { get; set; }

        /// <summary>
        /// Returns all of the Workflow triggers within the workflow definition
        /// </summary>
        public virtual IEnumerable<WorkflowTrigger<T>> PermittedTriggers => _stateMachine.PermittedTriggers;

        /// <summary>
        /// Changes the current state to a new state using the id of the trigger from the workflow definition collection.
        /// </summary>
        /// <param name="trigger"></param>
        public virtual void ChangeState(T trigger)
        {
            if (_workflowDefinition.Triggers.TryGetValue(trigger, out var workflowTrigger))
            {
                ChangeState(workflowTrigger);
            }
        }

        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
        /// <param name="trigger"></param>
        public virtual void ChangeState(WorkflowTrigger<T> trigger)
        {
            Guard.ArgumentIsNotNull(trigger, nameof(trigger));
            _stateMachine.Fire(trigger);
        }

        /// <summary>
        /// Determines whether this workflow can change its state based on the trigger.
        /// /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public virtual bool CanChangeState(WorkflowTrigger<T> trigger)
        {
            Guard.ArgumentIsNotNull(trigger, nameof(trigger));
            return _stateMachine.CanFire(trigger);
        }

        /// <summary>
        /// Returns workflow representation in a DotGraph format.
        /// </summary>
        /// <returns></returns>
        public virtual string ToDotGraph()
        {
            return UmlDotGraph.Format(_stateMachine.GetInfo());
        }
        #endregion

        /// <summary>
        /// On workflow transition.
        /// </summary>
        /// <param name="transition"></param>
        protected virtual void OnTransitionAction(StateMachine<WorkflowState<T, TContext>, WorkflowTrigger<T>>.Transition transition)
		{
			// Find the states
			var sourceState = transition.Source;
			var destinationState = transition.Destination;

            if (sourceState == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find the source state {0} in the list of available states.", transition.Source));
            }

            if (destinationState == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find the destination state {0} in the list of available states.", transition.Destination));
            }

            // Find the trigger
            var trigger = transition.Trigger;

			if (trigger == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find the trigger {0} in the list of available triggers.", transition.Trigger));
            }

            var returnTransition = new WorkflowTransition<T, TContext>(sourceState, destinationState, trigger, transition.IsReentry);
			_onTransitionedEvent.Invoke(returnTransition);

            //Raise the event to the consumer
            OnStateTransition?.Invoke(sourceState, destinationState, trigger, transition.IsReentry);
        }

		/// <summary>
		/// Registers a callback that will be invoked every time the statemachine
		/// transitions from one state into another.
		/// </summary>
		/// <param name="onTransitionAction">The action to execute, accepting the details
		/// of the transition.
        /// </param>
		public virtual void OnWorkflowTransitioned(Action<WorkflowTransition<T, TContext>> onTransitionAction)
		{
			Guard.ArgumentIsNotNull(onTransitionAction, nameof(onTransitionAction));
			_onTransitionedEvent.Register(onTransitionAction);
		}

        /// <summary>
        /// Execute the IWorkflowGuard delegate.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Func<bool> ConditionalGuard(IWorkflowGuard<T, TContext> condition)
        {
            return () => condition.IsAllowed(this);
        }

        /// <summary>
        /// Execute IActions based on the workflow definition
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="entryAction"></param>
        private void ExecuteAction(StateMachine<WorkflowState<T, TContext>,
                                   WorkflowTrigger<T>>.Transition transition,
                                   IAction<T, TContext> entryAction)
        {
            if (entryAction == null)
            {
                return;
            }

            WorkflowTransition<T, TContext> wt = null;
            if (transition != null)
            {
                wt = new WorkflowTransition<T, TContext>(transition.Source,
                                                        transition.Destination,
                                                        transition.Trigger,
                                                        transition.IsReentry);
            }
            entryAction.Execute(this,wt);
        }

        /// <summary>
        /// Allows for the un-handled triggers to be tracked
        /// </summary>
        /// <param name="state"></param>
        /// <param name="trigger"></param>
        protected virtual void OnUnhandledTrigger(WorkflowState<T, TContext> state, WorkflowTrigger<T> trigger)
        {
            OnTriggerUnhandled?.Invoke(state, trigger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Autoflow.WorkflowEngine{T,TContext}"/> class.
        /// </summary>
        /// <param name="workflowDefinition"></param>
        /// <param name="currentState"></param>
        /// <param name="workflowContext"></param>
        public WorkflowEngine(WorkflowDefinition<T, TContext> workflowDefinition,
                              WorkflowState<T, TContext> currentState,
                              TContext workflowContext)
        {
            Guard.ArgumentIsNotNull(workflowDefinition, nameof(workflowDefinition));

            _onTransitionedEvent = new OnWorkflowTransitionedEvent<T, TContext>();
            _workflowContext = workflowContext;

            _workflowDefinition = workflowDefinition;
            CurrentState = currentState ?? workflowDefinition.Transitions.First().FromState;
            _stateMachine = new StateMachine<WorkflowState<T, TContext>, WorkflowTrigger<T>>(() => CurrentState, s => CurrentState = s);

            //  Get a distinct list of states with a trigger from state configuration
            //  "State => Trigger => TargetState
            var states = workflowDefinition.Transitions.AsQueryable()
                .Select(x => x.FromState)
                .Distinct()
                .Select(x => x)
                .ToList();

            //  Assign triggers to states
            foreach (var state in states)
            {
                var triggers = workflowDefinition.Transitions.AsQueryable()
                                                            .Where(config => config.FromState == state)
                                                            .Select(config => new
                                                            {
                                                                Trigger = config.TriggeredBy,
                                                                TargetState = config.ToState,
                                                                GuardCondition = config.GuardCondition,
                                                                IsReentrant = config.IsReentry
                                                            })
                                                            .ToList();

                var stateConfig = _stateMachine.Configure(state);

                foreach (var trig in triggers)
                {
                    if (trig.GuardCondition == null)
                    {
                        if (trig.IsReentrant)
                        {
                            stateConfig.PermitReentry(trig.Trigger);
                        }
                        else
                        {
                            if (trig.Trigger != null)
                            {
                                stateConfig.Permit(trig?.Trigger, trig?.TargetState);
                            }
                        }
                    }
                    else
                    {
                        if (trig.Trigger != null)
                        {
                            stateConfig.PermitIf(trig?.Trigger, trig?.TargetState, ConditionalGuard(trig?.GuardCondition));
                        }
                    }
                }

                //actions
                //var actions = workflowDefinition.States.Where();

                if (state.SuperState != null)
                {
                    stateConfig.SubstateOf(state.SuperState);
                }

                if (state.EntryAction != null)
                {
                    stateConfig.OnEntry(t => ExecuteAction(t, state.EntryAction));
                }

                if (state.ExitAction != null)
                {
                    stateConfig.OnExit(t => ExecuteAction(t, state.ExitAction));
                }
            }
            // Handle exceptions
            _stateMachine.OnUnhandledTrigger(OnUnhandledTrigger);

            // For all the state transitions
            _stateMachine.OnTransitioned(OnTransitionAction);
        }
    }
}
