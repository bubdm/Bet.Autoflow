using Bet.Autoflow;
using Shouldly;
using Xunit;

namespace xUnitTest
{
    public class CollectionTests
    {
        [Fact]
        public void WorkflowStatesCollectionNotNull()
        {
            //assign
            var sut = new WorkflowStateCollection<string,Customer>();
            //assert
            sut.ShouldNotBeNull();
        }

        [Fact]
        public void WorkflowStateCollectionAddNew()
        {
            //assign
            var sut = new WorkflowStateCollection<string,Customer>();
            var state = new WorkflowState<string,Customer>("state1") {Description ="Desc" };
            //act
            sut.Add(state);
            //assert
            sut.Count.ShouldBe(1);
        }

        [Fact]
        public void WorkflowTriggerCollectionNotNull()
        {
            //assign
            var sut = new WorkflowTriggerCollection<string>();
            //assert
            sut.ShouldNotBeNull();
        }

        [Fact]
        public void WorkflowTriggerCollectionAddNew()
        {
            var sut = new WorkflowTriggerCollection<string>();
            var t = new WorkflowTrigger<string>("trigger1");
            sut.Add(t);
            sut.Count.ShouldBe(1);
        }

        [Fact]
        public void WorkflowTransitionCollectionNotNull()
        {
            //assign
            var sut = new WorkflowTransitionCollection<string,Customer>();
            //assert
            sut.ShouldNotBeNull();
        }

        [Fact]
        public void WorkflowTransitionCollectionAddNew()
        {
            var sut = new WorkflowTransitionCollection<string,Customer>();
            var t = new WorkflowTransition<string,Customer>(new WorkflowState<string, Customer>("Start"),
                                                            new WorkflowState<string,Customer>("End"),
                                                            new WorkflowTrigger<string>("trigger1"));
            sut.Add(t);
            sut.Count.ShouldBe(1);
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
