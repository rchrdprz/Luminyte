using UnityEngine.AI;
using UnityEngine;

public class SeekerStateMachine : StateMachine
{
    [Header("Scene References")]
    public Transform Player;

    [Header("State Materials")]
    public Material IdleMaterial;
    public Material ChaseMaterial;
    public Material SearchMaterial;

    [Header("Prefab References")]
    public SpriteRenderer Sprite;
    public Transform Rotator;

    [Header("LayerMask")]
    public LayerMask WhatIsRelavant;

    public Vector3 StartLoc { get; private set; }

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public SeekerIdleState IdleState;
    [HideInInspector] public SeekerChaseState ChaseState;
    [HideInInspector] public SeekerSearchState SearchState;

    public override SeekerState InitialState => IdleState;

    private void Awake()
    {
        IdleState = new(this);
        ChaseState = new(this);
        SearchState = new(this);

        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        StartLoc = transform.position;
    }
}