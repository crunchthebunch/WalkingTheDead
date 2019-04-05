using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    [SerializeField] HumanSettings stats;

    bool isFleeing;

    Scanner zombieScanner;
    NavMeshAgent agent;
    WanderBehaviour wanderBehaviour;
    FleeBehaviour fleeBehaviour;

    public NavMeshAgent Agent { get => agent; }
    public HumanSettings Stats { get => stats; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public WanderBehaviour WanderBehaviour { get => wanderBehaviour; }
    public FleeBehaviour FleeBehaviour { get => fleeBehaviour; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.MovementSpeed;

        // Create Scanner
        zombieScanner = GetComponent<Scanner>();

        // Add Wander Component
        wanderBehaviour = gameObject.AddComponent<WanderBehaviour>();
        wanderBehaviour.SetupComponent(stats);

        fleeBehaviour = gameObject.AddComponent<FleeBehaviour>();
        fleeBehaviour.SetupComponent(stats);

        isFleeing = false;
        
    }

    private void Start()
    {
        zombieScanner.SetupScanner("Zombie", stats.Vision);
        // Scan around for zombies continuously
        // If there are no zombies, wander around
        // If there are zombies, flee
        // If out of zone AND not fleeing, go back to standard zone
        // StartCoroutine(FleeFromClosestEnemy());
    }

    private void Update()
    {
        
    }

}
