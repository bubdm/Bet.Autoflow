using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Autoflow;

namespace xUnitTest
{
    public static class Mock
    {
        public static WorkflowDefinition<string,Customer> SimpleWorkflowDefinition()
        {
            var wfDef = new WorkflowDefinition<string, Customer>("simpleWorkflow");

            wfDef.States.Add(new WorkflowStartState<string, Customer>("new"));
            wfDef.States.Add(new WorkflowState<string, Customer>("processing"));
            wfDef.States.Add(new WorkflowEndState<string, Customer>("closed"));

            wfDef.Triggers.AddRange(new List<WorkflowTrigger<string>> {
                new WorkflowTrigger<string> ("progress"),
                new WorkflowTrigger<string> ("close")
            });

            wfDef.Transitions.AddRange(new List<WorkflowTransition<string, Customer>> {
                new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["new"],
                    ToState = wfDef.States ["processing"],
                    TriggeredBy = wfDef.Triggers ["progress"]
                },
                 new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["processing"],
                    ToState = wfDef.States ["closed"],
                    TriggeredBy = wfDef.Triggers ["close"]
                },
            });
            return wfDef;
        }

        public static WorkflowDefinition<string,Customer> SingleActionWorkflowDefinition()
        {
            var actionList = new List<IAction<string, Customer>>
            {
                new ActionOne(),
                new ActionTwo()
            };

            var wfDef = new WorkflowDefinition<string, Customer>("simpleWorkflow");

            wfDef.States.Add(new WorkflowStartState<string, Customer>("new"));
            wfDef.States.Add(new WorkflowState<string, Customer>("action1",new ActionOne()));
            wfDef.States.Add(new WorkflowState<string, Customer>("action2", new ActionTwo()));
            wfDef.States.Add(new WorkflowEndState<string, Customer>("closed", new ActionThree()));

            wfDef.Triggers.AddRange(new List<WorkflowTrigger<string>> {
                new WorkflowTrigger<string> ("acted1"),
                new WorkflowTrigger<string> ("acted2"),
                new WorkflowTrigger<string> ("close")
            });

            wfDef.Transitions.AddRange(new List<WorkflowTransition<string, Customer>> {
                new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["new"],
                    ToState = wfDef.States ["action1"],
                    TriggeredBy = wfDef.Triggers ["acted1"]
                },
                new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["action1"],
                    ToState = wfDef.States ["action2"],
                    TriggeredBy = wfDef.Triggers ["acted2"]
                },
                new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["action2"],
                    ToState = wfDef.States ["closed"],
                    TriggeredBy = wfDef.Triggers ["close"]
                },
                new WorkflowTransition<string,Customer> {
                    FromState = wfDef.States ["closed"],
                },
            });
            return wfDef;
        }
    }
}
