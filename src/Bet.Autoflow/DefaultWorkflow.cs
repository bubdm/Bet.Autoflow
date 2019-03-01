using System;
using System.Collections.Generic;
using System.Linq;

namespace Bet.Autoflow
{
    /// <summary>
    /// The default implementation of the worklow engine
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultWorkflow<T> : ISpecializedBy<string>, IWorkflow<string, T>
    {
        private readonly WorkflowEngine<string, T> workflowEngine;

        /// <summary>
        /// Work flow id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Returns all of the Workflow triggers within the workflow definition
        /// </summary>
        public virtual IEnumerable<WorkflowTrigger<string>> PermittedTriggers => workflowEngine.PermittedTriggers;

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value> The state of the workflow.</value>
        public virtual WorkflowState<string, T> CurrentState => workflowEngine.CurrentState;

        /// <summary>
        /// Returns the context of the workflow.
        /// </summary>
        public T Context { get; set; }

        /// <summary>
        /// Change state of the workflow.
        /// </summary>
        /// <param name="trigger"></param>
        public virtual void ChangeState(string trigger)
        {
           workflowEngine.ChangeState(trigger);
        }

        /// <summary>
        /// Change state of the workflow.
        /// </summary>
        /// <param name="trigger"></param>
        public virtual void ChangeState(WorkflowTrigger<string> trigger)
        {
            workflowEngine.ChangeState(trigger);
        }

        /// <summary>
        /// Can change the workflow state
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public virtual bool CanChangeState(WorkflowTrigger<string> trigger)
        {
            return workflowEngine.CanChangeState(trigger);
        }

        /// <summary>
        /// The Uml DotGraph representation.
        /// </summary>
        /// <returns></returns>
        public virtual string ToDotGraph()
        {
            return workflowEngine.ToDotGraph();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="wd"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public DefaultWorkflow(
            string id,
            WorkflowDefinition<string, T> wd,
            T context)
        {
            Id = id;
            Context = context;
            workflowEngine = new WorkflowEngine<string, T>(wd, null, context);
            workflowEngine.OnStateTransition += WorkflowEngine_OnStateTransition;
        }

        /// <summary>
        /// Provides entry point to the transitions, can be used for logging
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="trigger"></param>
        /// <param name="reentery"></param>
        public virtual void WorkflowEngine_OnStateTransition(WorkflowState<string, T> source,
                                                             WorkflowState<string, T> destination,
                                                             WorkflowTrigger<string> trigger, bool reentery)
        {
        }

        /// <summary>
        /// Completes all of the Triggers in the workflow
        /// </summary>
        /// <returns></returns>
        public virtual bool Complete()
        {
            try
            {
                //create auto steps
                while (workflowEngine.PermittedTriggers.Any())
                {
                    var currentState = workflowEngine.CurrentState;
                    var nextState = workflowEngine.PermittedTriggers.First();

                    //move to the next state
                    workflowEngine.ChangeState(nextState);
                }
                return true;
            }
            catch (Exception)
            {
                // TODO: revisit the capture the exceptions.
                return false;
                //throw;
            }
        }
    }
}
