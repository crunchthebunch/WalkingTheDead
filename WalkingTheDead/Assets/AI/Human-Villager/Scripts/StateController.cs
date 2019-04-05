using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // [SerializeField] HumanStats stats;
    [SerializeField] State currentState;
    [SerializeField] State remainState;

    HumanSettings stats;
    Villager owner;

    public Villager Owner { get => owner; }
    public HumanSettings Stats { get => stats; }

    // Start is called before the first frame update
    void Awake()
    {
        owner = GetComponent<Villager>();
        stats = owner.Stats;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If Ai active update it's state
        currentState.UpdateState(this);
    }

    // Only for testing
    public void Test()
    {
        print("Enemies Around.");
    }

    public void TestOpposite()
    {
        print("No Enemies Around.");
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
