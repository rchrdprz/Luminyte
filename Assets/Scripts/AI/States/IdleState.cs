using UnityEngine.AI;
using UnityEngine;

[System.Serializable]
public class IdleState : State
{
    private Transform _player;
    private NavMeshAgent _agent;
    private NPCStateMachine _npcStateMachine;
    
    private readonly float _radius = 10;
    private readonly float _delayMin = 1;
    private readonly float _delayMax = 5;
    private readonly float _viewDst = 6;

    private Vector3 _startLoc;
    private Vector3 _nextLoc;
    private float _waitTimer;

    public IdleState(NPCStateMachine stateMachine) : base("Idle", stateMachine) 
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        //_npcStateMachine.Sprite.material = _npcStateMachine.IdleMaterial;

        _agent = _npcStateMachine.Agent;
        _player = _npcStateMachine.Player;
        _startLoc = _npcStateMachine.StartLoc;

        _waitTimer = Random.Range(_delayMin, _delayMax);

        GetPosition();
        _agent.SetDestination(_nextLoc);
    }

    public override void Update()
    {
        base.Update();

        Transition();
        Movement();
        Rotation();
    }

    private void Movement()
    {
        if (Vector2.Distance(_agent.transform.position, _nextLoc) > 1f) return;

        if (_waitTimer < 0)
        {
            GetPosition();
            _agent.SetDestination(_nextLoc);

            _waitTimer = Random.Range(_delayMin, _delayMax);
        }
        else _waitTimer -= Time.deltaTime;
    }

    private void Transition()
    {
        float _distance = Vector2.Distance(_agent.transform.position, _player.position);
        if (_distance > _viewDst) return;

        _npcStateMachine.ChangeState(_npcStateMachine.ChaseState);
    }

    private void Rotation()
    {
        if (Vector3.Distance(_agent.steeringTarget, _agent.transform.position) < 1f) return;

        Vector3 direction = _agent.steeringTarget - _agent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        _npcStateMachine.Rotator.rotation = Quaternion.Lerp(_npcStateMachine.Rotator.rotation, rotation, 0.025f);
    }

    private void GetPosition()
    {
        bool isCalculating = true;
        Vector3 newLocation = Vector3.zero;
        NavMeshPath path = new();

        while (isCalculating)
        {
            newLocation = new(_startLoc.x + Random.Range(-_radius, _radius), _startLoc.y + Random.Range(-_radius, _radius));
            _agent.CalculatePath(newLocation, path);

            if (path.status != NavMeshPathStatus.PathInvalid) isCalculating = false;
        }

        _nextLoc = newLocation;
    }
}