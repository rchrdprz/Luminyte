using UnityEngine.AI;
using UnityEngine;

public class SeekerChaseState : SeekerState
{
    private Vector3 _lastKnownLoc;

    public SeekerChaseState(SeekerStateMachine stateMachine) : base("Chase", stateMachine)
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _lastKnownLoc = _player.transform.position;

        _npcStateMachine.Sprite.material = _npcStateMachine.ChaseMaterial;

        _agent.SetDestination(_player.transform.position);
    }

    public override void Update()
    {
        base.Update();

        Transition(_agent, _npcStateMachine, _viewDst);
        Rotation(_agent, _npcStateMachine);
    }

    public override void Transition(NavMeshAgent agent, SeekerStateMachine stateMachine, float view)
    {
        base.Transition(agent, stateMachine, view);

        if (_hit == true && _hit.transform.TryGetComponent<PlayerMovement>(out _))
        {
            _lastKnownLoc = _player.transform.position;
            _agent.SetDestination(_lastKnownLoc);

        }
        else
        {
            if (Vector2.Distance(_lastKnownLoc, _agent.transform.position) < 1f)
                _npcStateMachine.ChangeState(_npcStateMachine.SearchState);
        }       
    }
}