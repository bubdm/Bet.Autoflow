using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Autoflow;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace xUnitTest
{
    public class WorkflowEngineTests
    {
        #region xUnit setup
        private readonly ITestOutputHelper output;

        public WorkflowEngineTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        #endregion

        [Fact]
        public void WorkflowEngine_Execute_Auto_Simple()
        {
            var wd = Mock.SimpleWorkflowDefinition();

            var context = new Customer { Id = 1, Name = "Customer Name" };

            var wf = new WorkflowEngine<string, Customer>(wd, null, context);

            wf.OnStateTransition += onStateTransition;

            var result = wf.PermittedTriggers;

            result.ShouldNotBeNull();

            //create auto steps
            while (wf.PermittedTriggers.Any())
            {
                var currentState = wf.CurrentState;
                var nextState = wf.PermittedTriggers.First();

                nextState.ShouldNotBeNull();
                //move to the next state
                wf.ChangeState(nextState);
            }
        }

        [Fact]
        public void WorkflowEngine_Execute_Auto_SimpleAction()
        {
            var wd = Mock.SingleActionWorkflowDefinition();

            var context = new Customer { Id = 1, Name = "Customer Name" };

            var wf = new WorkflowEngine<string, Customer>(wd, null, context);

            wf.OnStateTransition += onStateTransition;

            var result = wf.PermittedTriggers;

            result.ShouldNotBeNull();

            //create auto steps
            while (wf.PermittedTriggers.Any())
            {
                var currentState = wf.CurrentState;
                var nextState = wf.PermittedTriggers.First();

                nextState.ShouldNotBeNull();
                //move to the next state
                wf.ChangeState(nextState);
            }
        }

        [Fact]
        public void DefaultWorkflow_Success()
        {
            var idWf = Guid.NewGuid().ToString();
            var customer = new Customer { Id = 2, Name = "Test Customer" };
            var wd = Mock.SimpleWorkflowDefinition();
            var wf = new DefaultWorkflow<Customer>(idWf, wd,customer);

            wf.Id.ShouldBe(idWf);

            var result = wf.Complete();

            result.ShouldBe(true);
        }

        private void onStateTransition(WorkflowState<string, Customer> source,
                                       WorkflowState<string, Customer> destination,
                                       WorkflowTrigger<string> trigger, bool reentry)
        {
            if (source is WorkflowStartState<string,Customer>)
                output.WriteLine("Workflow started");

            if (destination is WorkflowEndState<string,Customer>)
                output.WriteLine("Workflow completed");

            output.WriteLine($"{DateTime.Now.ToShortTimeString()}: State changed from {source} to {destination} because of trigger {trigger}.");
        }
    }
}
