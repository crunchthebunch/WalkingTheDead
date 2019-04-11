using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] ZombieSettings settings = null;
    Scanner humanScanner;
    NavMeshAgent agent;
    ChaseZombieBehaviour chaseBehaviour;
    WanderZombieBehaviour wanderBehaviour;
    MoveToZombieBehaviour moveToBehaviour;
    AttackZombieBehaviour attackBehaviour;


    Vector3 desiredPosition;
    bool commandGiven;
    bool followPlayer;
    GameObject player;

    public ZombieSettings Settings { get => settings; }
    public Scanner HumanScanner { get => humanScanner; }
    public NavMeshAgent Agent { get => agent; }
    public ChaseZombieBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public Vector3 DesiredPosition { get => desiredPosition; set => desiredPosition = value; }
    public WanderZombieBehaviour WanderBehaviour { get => wanderBehaviour;}
    public bool CommandGiven { get => commandGiven; set => commandGiven = value; }
    public MoveToZombieBehaviour MoveToBehaviour { get => moveToBehaviour;}
    public bool FollowPlayer { get => followPlayer; set => followPlayer = value; }
    public GameObject Player { get => player;}
    public AttackZombieBehaviour AttackBehaviour { get => attackBehaviour;}
    public Animator Anim { get => anim; set => anim = value; }

    ZombieStateController controller;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("PlayerCharacter");
        desiredPosition = transform.position;
        commandGiven = false;

        // Setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        // Add Scanner
        humanScanner = transform.Find("HumanScanner").GetComponent<Scanner>();

        // Add Chase Behaviour
        chaseBehaviour = gameObject.AddComponent<ChaseZombieBehaviour>();

        // Add wander behaviour
        wanderBehaviour = gameObject.AddComponent<WanderZombieBehaviour>();

        //Add moveto Behaviour
        moveToBehaviour = gameObject.AddComponent<MoveToZombieBehaviour>();

        //Add Attack Behaviour
        attackBehaviour = gameObject.AddComponent<AttackZombieBehaviour>();
    }

    private void Start()
    {
        humanScanner.SetupScanner("Human", settings.Vision);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) desiredPosition = player.transform.position;

        if (agent.velocity.x != 0 || agent.velocity.z != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void OnEnable()
    {
        PlayerCommand.Click += RecieveCommand;
    }

    private void OnDisable()
    {
        PlayerCommand.Click -= RecieveCommand;
    }

    void RecieveCommand(Vector3 position ,bool followPlayer)
    {
        desiredPosition = position;
        agent.SetDestination(desiredPosition);
        commandGiven = true;
        this.followPlayer = followPlayer;
    }
    void KillClosestHuman()
    {
        GameObject toKill = humanScanner.GetClosestTargetInRange();

        if (toKill != null && Mathf.Abs(Vector3.Distance(transform.position, toKill.transform.position)) < Settings.AttackRange)
        {
            humanScanner.ObjectsInRange.Remove(toKill);
            Destroy(toKill);
            toKill = null;
        }
    }
}
