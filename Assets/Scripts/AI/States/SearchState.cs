using UnityEngine.AI;
using UnityEngine;

public class SearchState : State
{
    private Transform _player;
    private NavMeshAgent _agent;
    private NPCStateMachine _npcStateMachine;

    private Vector3 _nextLoc;
    private Vector3 _lastKnownLoc;
    private Vector2 _direction;

    private readonly float _radius = 5;
    private readonly float _delayMin = 1;
    private readonly float _delayMax = 5;
    private readonly float _viewDst = 5;
    private readonly float _lookTime = 2;

    private float _waitTimer;
    private float _lookTimer;
    private float _searchTimes;

    private bool _isFirstSearch = true;

    public SearchState(NPCStateMachine stateMachine) : base("Search", stateMachine) 
    {
        _npcStateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _agent = _npcStateMachine.Agent;
        _player = _npcStateMachine.Player;

        _lastKnownLoc = _agent.transform.position;
        _waitTimer = Random.Range(_delayMin, _delayMax);

        _searchTimes = Random.Range(1, 5);
        _isFirstSearch = true;
    }

    public override void Update()
    {
        base.Update();

        FirstSearch();
        SearchCheck();
        Rotation();
    }

    private void SearchCheck()
    {
        float _distance = Vector2.Distance(_agent.transform.position, _player.position);
        if (_distance > _viewDst)
        {
            Search();
            return;
        }

        Vector2 direction = _player.position - _agent.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(_agent.transform.position, direction, 15f, ~(1 << 7));

        if (hit == true && hit.transform.TryGetComponent<PlayerMovement>(out _))
        {
            _npcStateMachine.ChangeState(_npcStateMachine.ChaseState);
        }
        else Search();
    }

    private void Search()
    {
        if (_isFirstSearch || Vector2.Distance(_agent.transform.position, _nextLoc) > 1f) return;

        if (_waitTimer < 0)
        {
            _agent.SetDestination(GetPosition());
            _waitTimer = Random.Range(_delayMin, _delayMax);

            if (_searchTimes <= 0)
            {
                _npcStateMachine.ChangeState(_npcStateMachine.IdleState);
            } 
            else _searchTimes--;
        }
        else _waitTimer -= Time.deltaTime;
    }

    private void Rotation()
    {
        if (Vector3.Distance(_agent.steeringTarget, _agent.transform.position) > 1f)
        {
            Vector3 direction = _agent.steeringTarget - _agent.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            _npcStateMachine.Graphics.rotation = Quaternion.Lerp(_npcStateMachine.Graphics.rotation, rotation, 0.025f);
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
            _npcStateMachine.Graphics.rotation = Quaternion.Lerp(_npcStateMachine.Graphics.rotation, rotation, 0.025f);
        }
    }

    private void FirstSearch()
    {
        if (!_isFirstSearch) return;

        if (_waitTimer < 0)
        {
            _isFirstSearch = false;
            _agent.SetDestination(GetPosition());

            _waitTimer = Random.Range(_delayMin, _delayMax);
        }
        else _waitTimer -= Time.deltaTime;

        if (!_isFirstSearch) 
            _agent.SetDestination(GetPosition());
    }

    private Vector3 GetPosition()
    {
        bool isCalculating = true;
        Vector3 newLocation = Vector3.zero;
        NavMeshPath path = new();

        while (isCalculating)
        {
            newLocation = new(_lastKnownLoc.x + Random.Range(-_radius, _radius), _lastKnownLoc.y + Random.Range(-_radius, _radius));

            _agent.CalculatePath(newLocation, path);
            if (path.status != NavMeshPathStatus.PathInvalid) isCalculating = false;
        }

        _nextLoc = newLocation;
        return newLocation;
    }
}