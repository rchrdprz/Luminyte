using UnityEngine.AI;
using UnityEngine;

public class SeekerState
{
    private string _name;
    protected StateMachine _stateMachine;

    protected readonly float _radius = 5;
    protected readonly float _viewDst = 3;
    protected readonly Vector2 _delay = new(1, 5);

    protected Transform _player;
    protected NavMeshAgent _agent;
    protected SeekerStateMachine _npcStateMachine;

    protected RaycastHit2D _hit;

    public string Name => _name;

    public SeekerState(string name, StateMachine stateMachine)
    {
        _name = name;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() 
    {
        _agent = _npcStateMachine.Agent;
        _player = _npcStateMachine.Player;
    }

    public virtual void Update() { }

    public virtual void Exit() { }

    public virtual void Transition(NavMeshAgent agent, SeekerStateMachine stateMachine, float view)
    {
        Vector2 direction = stateMachine.Player.position - agent.transform.position;
        _hit = Physics2D.Raycast(agent.transform.position, direction, view, stateMachine.WhatIsRelavant);
        Debug.DrawRay(_agent.transform.position, direction * _viewDst, Color.red);
    }

    public Vector3 GetPosition(NavMeshAgent agent, Vector3 location, float radius)
    {
        NavMeshPath path = new();
        bool isCalculating = true;
        Vector3 newLocation = Vector3.zero;

        while (isCalculating)
        {
            newLocation = new(location.x + Random.Range(-radius, radius), location.y + Random.Range(-radius, radius));

            agent.CalculatePath(newLocation, path);
            if (agent.path.status != NavMeshPathStatus.PathInvalid) isCalculating = false;
        }

        return newLocation;
    }

    public virtual void Rotation(NavMeshAgent agent, SeekerStateMachine stateMachine)
    {
        if (Vector3.Distance(agent.steeringTarget, agent.transform.position) < 1f) return;

        Vector3 direction = agent.steeringTarget - agent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        stateMachine.Rotator.rotation = Quaternion.Lerp(stateMachine.Rotator.rotation, rotation, 0.025f);
    }
}