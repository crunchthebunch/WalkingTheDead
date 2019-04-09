using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    [SerializeField] VillagerSettings settings = null;
    Scanner zombieScanner;
    NavMeshAgent agent;

    WanderVillagerBehaviour wanderBehaviour;
    FleeVillagerBehaviour fleeBehaviour;
    MoveBackVillagerBehaviour moveBackBehaviour;

    VillagerStateController controller;
    Animator anim;

    public NavMeshAgent Agent { get => agent; }
    public VillagerSettings Settings { get => settings; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public WanderVillagerBehaviour WanderBehaviour { get => wanderBehaviour; }
    public FleeVillagerBehaviour FleeBehaviour { get => fleeBehaviour; }
    public MoveBackVillagerBehaviour MoveBackBehaviour { get => moveBackBehaviour; }

    private void Awake()
    {
        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        anim = GetComponentInChildren<Animator>();

        // Create Scanner
        zombieScanner = GetComponentInChildren<Scanner>();

        // Add Wander Component
        wanderBehaviour = gameObject.AddComponent<WanderVillagerBehaviour>();
        wanderBehaviour.SetupComponent(settings);

        // Add Flee Behaviour
        fleeBehaviour = gameObject.AddComponent<FleeVillagerBehaviour>();
        fleeBehaviour.SetupComponent(settings);

        // Add Moving Back Behaviour
        moveBackBehaviour = gameObject.AddComponent<MoveBackVillagerBehaviour>();
        moveBackBehaviour.SetupComponent(settings);

        // Get the controller - TODO might want to add this component and set it up later on
        controller = GetComponent<VillagerStateController>();
    }

    private void Start()
    {
        zombieScanner.SetupScanner("Zombie", settings.Vision);
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (agent.speed == settings.FleeSpeed && !agent.isStopped)
        {
            anim.SetBool("isRunning", true);
        }
        else if (agent.speed == settings.WalkingSpeed && !agent.isStopped)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }
        else if (agent.isStopped)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
    }
}