using UnityEngine.AI;
using UnityEngine;

[System.Serializable]
public class SeekerIdleState : SeekerState
{
    private Vector3 _startLoc;
    private Vector3 _nextLoc;
    private float _waitTimer;

    public SeekerIdleState(SeekerStateMachine stateMachine) : base("Idle", stateMachine) 
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _startLoc = _npcStateMachine.StartLoc;
        _waitTimer = Random.Range(_delay.x, _delay.y);

        _npcStateMachine.Sprite.material = _npcStateMachine.IdleMaterial;

        _nextLoc = GetPosition(_agent, _startLoc, _radius);
        _agent.SetDestination(_nextLoc);
    }

    public override void Update()
    {
        base.Update();

        Transition(_agent, _npcStateMachine, _viewDst);
        Movement();
        Rotation(_agent, _npcStateMachine);
    }

    private void Movement()
    {
        if (Vector2.Distance(_agent.transform.position, _nextLoc) > 1f) return;

        if (_waitTimer < 0)
        {
            _nextLoc = GetPosition(_agent, _startLoc, _radius);
            _agent.SetDestination(_nextLoc);

            _waitTimer = Random.Range(_delay.x, _delay.y);
        }
        else _waitTimer -= Time.deltaTime;
    }

    public override void Transition(NavMeshAgent agent, SeekerStateMachine stateMachine, float view)
    {
        float distance = Vector2.Distance(agent.transform.position, stateMachine.Player.position);
        if (distance > view) return;

        base.Transition(agent, stateMachine, view);

        if (_hit == true && _hit.transform.TryGetComponent<PlayerMovement>(out _))
            _npcStateMachine.ChangeState(_npcStateMachine.ChaseState);
    }
}