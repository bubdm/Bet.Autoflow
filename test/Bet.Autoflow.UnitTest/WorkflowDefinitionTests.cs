using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bet.Autoflow;
using Shouldly;
using Xunit;

namespace xUnitTest
{
    public class WorkflowDefinitionTests
    {
        [Fact]
        public void WorkflowDefinition()
        {
            var sut = new WorkflowDefinition<string,Customer>("workflow1");

            sut.States.Add(new WorkflowState<string,Customer>("state1"));
            sut.Version = Assembly.GetExecutingAssembly().GetName().Version;

            var result = sut.Serialize();
            result.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void WorkflowDefinitionIsValidSuccess()
        {
            var sut = new WorkflowDefinition<string, Customer>("workflow1");

            sut.States.Add(new WorkflowStartState<string, Customer>("start"));
            //sut.States.Add(new WorkflowStartState<string, Customer>("start2"));

            sut.States.Add(new WorkflowEndState<string, Customer>("end"));
            //sut.States.Add(new WorkflowEndState<string, Customer>("end2"));

            sut.Version = Assembly.GetExecutingAssembly().GetName().Version;

            var result = sut.IsValid();
            result.ShouldBe(true);
        }

        [Fact]
        public void WorkflowDefitionCreateSuccess()
        {
            var wfDef = Mock.SimpleWorkflowDefinition();

            wfDef.IsValid().ShouldBe(true);

            var result = wfDef.Serialize();
            //TODO: double check the json generation
            result.ShouldNotBeNullOrEmpty();
        }
    }
}
