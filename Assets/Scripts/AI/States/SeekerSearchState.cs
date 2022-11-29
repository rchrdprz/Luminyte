using UnityEngine.AI;
using UnityEngine;

public class SeekerSearchState : SeekerState
{
    private Vector3 _nextLoc;
    private Vector3 _lastKnownLoc;
    private Vector2 _direction;

    private readonly float _lookTime = 2;

    private float _waitTimer;
    private float _lookTimer;
    private float _searchTimes;

    private bool _isFirstSearch = true;

    public SeekerSearchState(SeekerStateMachine stateMachine) : base("Search", stateMachine) 
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _lastKnownLoc = _agent.transform.position;
        _waitTimer = Random.Range(_delay.x, _delay.y);

        _searchTimes = Random.Range(2, 5);
        _isFirstSearch = true;
    }

    public override void Update()
    {
        base.Update();

        Transition(_agent, _npcStateMachine, _viewDst);
        Rotation();
    }

    public override void Transition(NavMeshAgent agent, SeekerStateMachine stateMachine, float view)
    {
        base.Transition(agent, stateMachine, view);

        if (_hit == true && _hit.transform.TryGetComponent<PlayerMovement>(out _))
        {
            _npcStateMachine.ChangeState(_npcStateMachine.ChaseState);
        }
        else Search();
    }

    private void Search()
    {
        if (!_isFirstSearch && Vector2.Distance(_agent.transform.position, _nextLoc) > 1f) 
        { 
            _npcStateMachine.Sprite.material = _npcStateMachine.SearchMaterial;
            return; 
        } 


        if (_waitTimer < 0)
        {
            _isFirstSearch = false;
            _nextLoc = GetPosition(_agent,_lastKnownLoc, _radius);
            _agent.SetDestination(_nextLoc);
            _waitTimer = Random.Range(_delay.x, _delay.y);

            if (_searchTimes <= 0)
            {
                _npcStateMachine.ChangeState(_npcStateMachine.IdleState);
            } 
            else _searchTimes--;
        }
        else _waitTimer -= Time.deltaTime;
    }

    public virtual void Rotation()
    {
        if (Vector3.Distance(_agent.steeringTarget, _agent.transform.position) > 1f)
        {
            Vector3 direction = _agent.steeringTarget - _agent.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            _npcStateMachine.Rotator.rotation = Quaternion.Lerp(_npcStateMachine.Rotator.rotation, rotation, 0.025f);
        }
        else
        {
            if (_lookTimer < 0)
            {
                _direction = new(Random.Range(-20, 20), Random.Range(-20, 20));
                _lookTimer = _lookTime;
            }
            else _lookTimer -= Time.deltaTime;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            _npcStateMachine.Rotator.rotation = Quaternion.Lerp(_npcStateMachine.Rotator.rotation, rotation, 0.025f);
        }
    }
}