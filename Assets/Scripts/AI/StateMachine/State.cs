public class State
{
    private string _name;
    protected StateMachine _stateMachine;

    public string Name => _name;

    public State(string name, StateMachine stateMachine)
    {
        _name = name;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}