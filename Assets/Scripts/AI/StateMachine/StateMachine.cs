using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private SeekerState _currentState;

    public virtual SeekerState InitialState => null;

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

    public void ChangeState(SeekerState newState)
    {
        _currentState?.Exit();

        _currentState = newState;
        _currentState?.Enter();
    }

    protected virtual SeekerState GetInitialState()
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