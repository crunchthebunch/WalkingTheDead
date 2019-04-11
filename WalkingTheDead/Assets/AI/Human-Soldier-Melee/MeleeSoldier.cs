using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeSoldier : MonoBehaviour
{
    [SerializeField] MeleeSoldierSettings settings = null;
    [SerializeField] List<GameObject> additionalPatrolpositions = new List<GameObject>();

    [SerializeField] GameObject [] deadBodies = null;

    PlayerResources gameManager;
    NavMeshAgent agent;
    Scanner zombieScanner;

    // Behaviours
    PatrolMeleeSoldierBehaviour patrolBehaviour;
    ChaseMeleeSoldierBehaviour chaseBehaviour;
    AttackMeleeSoldierBehaviour attackBehaviour;

    public MeleeSoldierSettings Settings { get => settings; }
    public NavMeshAgent Agent { get => agent; }

    // Behaviours
    public PatrolMeleeSoldierBehaviour PatrolBehaviour { get => patrolBehaviour; }
    public ChaseMeleeSoldierBehaviour ChaseBehaviour { get => chaseBehaviour; }
    public AttackMeleeSoldierBehaviour AttackBehaviour { get => attackBehaviour; }

    public List<GameObject> AdditionalPatrolpositions { get => additionalPatrolpositions; }
    public Scanner ZombieScanner { get => zombieScanner; }
    public PlayerResources GameManager { get => gameManager; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.WalkingSpeed;

        gameManager = FindObjectOfType<PlayerResources>();

        // Add Scanner
        zombieScanner = transform.Find("ZombieScanner").GetComponent<Scanner>();

        // Add patrol behaviour
        patrolBehaviour = gameObject.AddComponent<PatrolMeleeSoldierBehaviour>();

        // Add chase behaviour
        chaseBehaviour = gameObject.AddComponent<ChaseMeleeSoldierBehaviour>();

        // Add attack behaviour
        attackBehaviour = gameObject.AddComponent<AttackMeleeSoldierBehaviour>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add 2 tags
        zombieScanner.SetupScanner("Zombie", settings.Vision);
        zombieScanner.SetupScanner("Necromancer", settings.Vision);
    }

    public void Die()
    {
        // Spawn a random dead body
        int bodyIndex = Random.Range(0, deadBodies.Length);
        Vector3 deadPosition = transform.position;
        deadPosition.y = transform.position.y - transform.localScale.y;

        Instantiate(deadBodies[bodyIndex], deadPosition, transform.rotation);

        // Simulate feeding
        gameManager.DecreaseHungerLevel();

        // Kill yourself
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 eyePosition = transform.position;
        eyePosition.y = transform.position.y + transform.localScale.y / 2f;

        Gizmos.DrawRay(eyePosition, transform.forward * settings.PatrolDistance);
    }

}
