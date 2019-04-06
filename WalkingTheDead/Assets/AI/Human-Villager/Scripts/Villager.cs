using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    [SerializeField] VillagerSettings settings;
    Scanner zombieScanner;
    NavMeshAgent agent;
    WanderBehaviour wanderBehaviour;
    FleeBehaviour fleeBehaviour;
    MoveBackBehaviour moveBackBehaviour;

    public NavMeshAgent Agent { get => agent; }
    public VillagerSettings Settings { get => settings; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public WanderBehaviour WanderBehaviour { get => wanderBehaviour; }
    public FleeBehaviour FleeBehaviour { get => fleeBehaviour; }
    public MoveBackBehaviour MoveBackBehaviour { get => moveBackBehaviour; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.MovementSpeed;

        // Create Scanner
        zombieScanner = GetComponent<Scanner>();

        // Add Wander Component
        wanderBehaviour = gameObject.AddComponent<WanderBehaviour>();
        wanderBehaviour.SetupComponent(settings);

        fleeBehaviour = gameObject.AddComponent<FleeBehaviour>();
        fleeBehaviour.SetupComponent(settings);

        moveBackBehaviour = gameObject.AddComponent<MoveBackBehaviour>();
        moveBackBehaviour.SetupComponent(settings);
        
    }

    private void Start()
    {
        zombieScanner.SetupScanner("Zombie", settings.Vision);
    }


}
