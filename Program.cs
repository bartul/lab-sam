// See https://aka.ms/new-console-template for more information

using System.Data;

class Program
{
    class Actions
    {
        public void SetModel(Model model)
        {
            _model = model;
        }
        private Model? _model;

        public void CountBy(int step = 1)
        {
            var proposal = new Proposal(step);
            _model?.Accept(proposal);
        }
    }
    class Model(State state, int counter = 0)
    {
        public int Counter { get; internal set; } = counter;

        public void Accept(Proposal proposal)
        {
            Counter += proposal.IncrementBy > 0 ? proposal.IncrementBy : 0;
            state.Render(this);
        }

    }
    class State(Actions actions)
    {
        public Actions Actions { get; } = actions;

        public void Render(Model model)
        {
            if (!NextAction(model))
            {
                Console.WriteLine($"Counter: {model.Counter}");
            }
        }
        public bool NextAction(Model model)
        {
            if (model.Counter % 2 == 0)
            {
                Actions.CountBy(1);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    public record Proposal(int IncrementBy);

    static void Main()
    {
        var actions = new Actions();
        var state = new State(actions);
        var model = new Model(state);
        actions.SetModel(model);

        actions.CountBy(2);
        actions.CountBy(5);


        Console.WriteLine("Done.");
    }
}

