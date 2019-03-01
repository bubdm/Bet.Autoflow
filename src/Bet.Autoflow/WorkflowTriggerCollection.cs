using System.Collections.Generic;

namespace Bet.Autoflow
{
    /// <summary>
    /// The trigger collection.
    /// </summary>
    public class WorkflowTriggerCollection<T> : Dictionary<T, WorkflowTrigger<T>>
    {
        /// <summary>
        ///  Adds a new trigger the collection.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        /// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
        public void Add(WorkflowTrigger<T> trigger)
        {
            Add(trigger.Id, trigger);
        }

        /// <summary>
        /// Add range of new triggers to the collection
        /// </summary>
        /// <param name="triggers"></param>
        public void AddRange(IEnumerable<WorkflowTrigger<T>> triggers)
        {
            foreach (var item in triggers)
            {
                Add(item);
            }
        }
    }
}
