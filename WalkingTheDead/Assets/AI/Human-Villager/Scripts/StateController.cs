using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // [SerializeField] HumanStats stats;
    [SerializeField] State currentState;
    [SerializeField] State remainState;
    [SerializeField] HumanSettings stats;

    Villager owner;
    bool aiActive;
    NavMeshAgent agent;
    Vector3 closestZombieLocation;

    public NavMeshAgent Agent { get => agent; }
    public Villager Owner { get => owner; }
    public HumanSettings Stats { get => stats; }
    public Vector3 ClosestZombieLocation { get => closestZombieLocation; set => closestZombieLocation = value; }

    // Start is called before the first frame update
    void Awake()
    {
        owner = GetComponent<Villager>();
        agent = GetComponent<NavMeshAgent>();

        stats = owner.Stats;
    }

    public void SetupAI(bool isActiveFromHuman)
    {
        // See whether the human is active --> Only do this if it is
        aiActive = isActiveFromHuman;

        // Enable / Disable navmesh agent based on active
        if (aiActive)
        {
            agent.enabled = true;
        }
        else
        {
            agent.enabled = false;
        }
    }

    private void Start()
    {
        SetupAI(owner.enabled); // Move to objects
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiActive)
            return;

        
        // If Ai active update it's state
        currentState.UpdateState(this);
    }

    // Only for testing
    public void Test()
    {
        print("Happening.");
    }

    public void TransitionToState(State nextState)
    {
        // Change the state if the next state is not remaining in state
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }
}
