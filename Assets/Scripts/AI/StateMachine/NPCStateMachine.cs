using UnityEngine.AI;
using UnityEngine;

public class NPCStateMachine : StateMachine
{
    [HideInInspector] public IdleState IdleState;
    [HideInInspector] public ChaseState ChaseState;
    [HideInInspector] public SearchState SearchState;

    [Header("Scene References")]
    public Transform Player;

    [Header("State Materials")]
    public Material IdleMaterial;
    public Material ChaseMaterial;
    public Material SearchMaterial;

    [Header("Prefab References")]
    public Transform Rotator;
    public SpriteRenderer Sprite;

    public Vector3 StartLoc { get; private set; }

    [HideInInspector] public NavMeshAgent Agent;

    public override State InitialState => IdleState;

    private void Awake()
    {
        IdleState = new IdleState(this);
        ChaseState = new ChaseState(this);
        SearchState = new SearchState(this);

        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        StartLoc = transform.position;
    }
}