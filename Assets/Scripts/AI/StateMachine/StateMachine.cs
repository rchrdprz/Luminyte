using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;

    public virtual State InitialState => null;

    private void Start()
    {
        _currentState = InitialState;
        _currentState?.Enter();
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState?.Update();
    }

    public void ChangeState(State newState)
    {
        _currentState?.Exit();

        _currentState = newState;
        _currentState?.Enter();
    }

    protected virtual State GetInitialState()
    {
        return null;
    }

    /*
    private void OnGUI()
    {
        string content = _currentState != null ? _currentState.Name : "(No Current State)";
        GUILayout.Label($"{content}");
    }
    */
}