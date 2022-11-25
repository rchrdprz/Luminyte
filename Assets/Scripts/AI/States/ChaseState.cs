using UnityEngine.AI;
using UnityEngine;

public class ChaseState : State
{
    private Transform _player;
    private NavMeshAgent _agent;
    private NPCStateMachine _npcStateMachine;

    private Vector3 _lastKnownLoc;

    public ChaseState(NPCStateMachine stateMachine) : base("Chase", stateMachine) 
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _agent = _npcStateMachine.Agent;
        _player = _npcStateMachine.Player;

        _lastKnownLoc = _player.transform.position;
    }

    public override void Update()
    {
        base.Update();

        Chase();
        Rotation();
    }

    private void Chase()
    {
        Vector2 direction = _player.position - _agent.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(_agent.transform.position, direction, 10f, ~(1 << 7));

        if (hit == true && hit.transform.TryGetComponent<PlayerMovement>(out _))
            _lastKnownLoc = _player.transform.position;

        if (Vector2.Distance(_agent.transform.position, _lastKnownLoc) < 0.1f)
        {
            _npcStateMachine.ChangeState(_npcStateMachine.SearchState);
        }
        else _agent.SetDestination(_lastKnownLoc);
    }

    private void Rotation()
    {
        if (Vector3.Distance(_agent.steeringTarget, _agent.transform.position) < 1f) return;

        Vector3 direction = _agent.steeringTarget - _agent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        _npcStateMachine.Graphics.rotation = Quaternion.Lerp(_npcStateMachine.Graphics.rotation, rotation, 0.025f);
    }
}