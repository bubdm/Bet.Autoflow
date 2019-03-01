using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Bet.Autoflow
{
	/// <summary>
	/// Defines business process in a workflow.
	/// </summary>
	public class WorkflowDefinition<T, TContext> : ISpecializedBy<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowDefinition{T,TContext}"/> class.
		/// </summary>
		/// <param name="id"></param>
		public WorkflowDefinition(T id)
		{
			Id = id;
			States = new WorkflowStateCollection<T, TContext>();
            Triggers = new WorkflowTriggerCollection<T>();
            Transitions = new WorkflowTransitionCollection<T, TContext>();
		}

		/// <summary>
		/// Gets or sets the id of this workflow.
		/// </summary>
		/// <value> The Identifier. </value>
		public T Id { get; set; }

		/// <summary>
        /// The current version of this workflow definition
        /// </summary>
        /// <value>The version.</value>
		public Version Version { get; set; }

		/// <summary>
		/// Serializes this instance to a json string value.
		/// </summary>
		/// <returns>A json serialization.</returns>
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),

				Formatting = Formatting.Indented,
				//ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		/// <summary>
		/// Converts the json back into a workflow definition.
		/// </summary>
		/// <param name="json">The json representation.</param>
		/// <returns>A workflow definition.</returns>
		public static WorkflowDefinition<T, TContext> Deserialize(string json)
		{
			Guard.ArgumentIsNotNull(json, "json");
			return JsonConvert.DeserializeObject<WorkflowDefinition<T, TContext>>(json);
		}

		/// <summary>
        /// Gets or sets the states. States define the workflow. Like a Create state, an Assigned state etc.
		/// Each state can have an entry and exit action(s).
        /// </summary>
        /// <value>The states</value>
		public WorkflowStateCollection<T, TContext> States { get; set; }

		/// <summary>Gets or sets the transitions. A transition describes from which source state to destination state the workflow can move based on a certain trigger.
		/// For example; from the state Review, using accept, to the state Accepted.
		/// Conditions can be used to guard the state transfer.</summary><value>The transitions.</value>
		public WorkflowTransitionCollection<T,TContext> Transitions { get; set; }

		/// <summary>Gets or sets the triggers. A trigger can be used to move from state to state. Like Rejecting, Accepting, Closing.</summary><value>The triggers.</value>
		public WorkflowTriggerCollection<T> Triggers { get; set; }

		/// <summary>
		/// A utility method to validate the definition of a workflow.
		/// </summary>
		/// <remarks>
		/// To be used before the definition is being saved.
		/// </remarks>
		/// <returns>True if valid, otherwise false.</returns>
		public bool IsValid()
		{
            // 1. StartState and EndState are mandatory and should be declared only once.
            var startCount = States.Values.Where(x => x.GetType() == typeof(WorkflowStartState<T, TContext>));
            var endCount = States.Values.Where(x => x.GetType() == typeof(WorkflowEndState<T, TContext>));

            return startCount.Count() == 1 && endCount.Count() == 1;
        }
	}
}
