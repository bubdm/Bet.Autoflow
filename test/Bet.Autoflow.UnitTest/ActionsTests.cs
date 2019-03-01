using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bet.Autoflow;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static System.Diagnostics.Debug;

namespace xUnitTest
{
    public class ActionsTests
    {
        private readonly ITestOutputHelper output;

        public ActionsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void AggregateActionTests()
        {
            //arrange
            var actionList = new List<IAction<string, Customer>>
            {
                new ActionOne(),
                new ActionTwo()
            };

            var sut = new AggregateAction<string, Customer>(actionList);

            var customer = new Customer { Id = 2, Name = "Test Customer" };

            var wf = new DefaultWorkflow<Customer>(Guid.NewGuid().ToString(),
                                                   Mock.SimpleWorkflowDefinition(),
                                                   customer);

            var graph = wf.ToDotGraph();
            graph.ShouldNotBeNullOrEmpty();
            output.WriteLine(graph);

            var trigger = wf.PermittedTriggers.First();
            var state = wf.CurrentState;

            var transition = new WorkflowTransition<string, Customer>();

            sut.Execute(wf,transition);
        }
    }

    public class ActionOne : IAction<string, Customer>
    {
        public void Execute(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
           WriteLine($"ActionOne action executed: {workflow.GetType().Name}. Context Object: {workflow.Context.GetType().Name}");
        }

        public async Task ExecuteAsync(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
            //TODO: implement async
            throw new NotImplementedException();
        }
    }

    public class ActionTwo : IAction<string, Customer>
    {
        public void Execute(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
            WriteLine($"ActionTwo action executed: {workflow.GetType().Name}. Context Object: {workflow.Context.GetType().Name}");
        }

        public async Task ExecuteAsync(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
            //TODO: implement async
            throw new NotImplementedException();
        }
    }

    public class ActionThree : IAction<string, Customer>
    {
        public void Execute(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
            WriteLine($"ActionThree action executed: {workflow.GetType().Name}. Context Object: {workflow.Context.GetType().Name}");
        }

        public async Task ExecuteAsync(IWorkflow<string, Customer> workflow, WorkflowTransition<string, Customer> transition)
        {
            //TODO: implement async
            throw new NotImplementedException();
        }
    }
}
