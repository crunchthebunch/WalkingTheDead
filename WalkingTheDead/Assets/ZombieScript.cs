using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum FollowTarget
{
    PLAYER,
    HUMANS,
    COMMAND
}

public class ZombieScript : MonoBehaviour
{
    Vector3 desiredPosition;
    Vector3 commandPosition;

    GameObject player;
    NavMeshAgent agent;
    
    List<Collider> humansInRange = new List<Collider>();
    Collider closestHuman;

    SphereCollider detectionRange;
    CapsuleCollider attackRange;

    Camera mainCamera;
    FollowTarget target = FollowTarget.PLAYER;



    private void Start()
    {
        player = GameObject.Find("PlayerCharacter");
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GetComponent<SphereCollider>();
        attackRange = GetComponent<CapsuleCollider>();
        mainCamera = GameObject.Find("PlayerCharacter/Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        ProcessInput();
        SetDesiredPosition();

        //Move to Desired position
        agent.SetDestination(desiredPosition);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
        {
            humansInRange.Add(other);
            GetClosestHuman();
            target = FollowTarget.HUMANS;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            for (int i = 0; i < humansInRange.Count; i++)
            {
                if (humansInRange[i] == other)
                {
                    humansInRange.RemoveAt(i);
                    if (humansInRange.Count == 0)
                    {
                        if (commandPosition == Vector3.negativeInfinity) target = FollowTarget.PLAYER;
                        else target = FollowTarget.COMMAND;
                    }
                    break;
                    
                }
            }
        }
    }

    void SetDesiredPosition()
    {
        if (target == FollowTarget.PLAYER)
        {
            desiredPosition = player.transform.position;
        }
        else if (target == FollowTarget.COMMAND)
        {
            desiredPosition = commandPosition;
        }
        else if (target == FollowTarget.HUMANS)
        {
            desiredPosition = closestHuman.transform.position;
        }
    }

    void GetClosestHuman()
    {
        closestHuman = humansInRange[0];
        
        for (int i = 1; i < humansInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, humansInRange[i].transform.position) < Vector3.Distance(transform.position, closestHuman.transform.position))
            {
                closestHuman = humansInRange[i];
            }
        }
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetCommandPosition();
            target = FollowTarget.COMMAND;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            target = FollowTarget.PLAYER;
            commandPosition = Vector3.negativeInfinity;
        }
    }

    void GetCommandPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            commandPosition = hitInfo.point;
        }
        else commandPosition = Vector3.negativeInfinity;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = new Color(0, 0, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, detectionRange.radius);
        Gizmos.color = new Color(1, 0.8f, 0.016f, 0.1f);
        Gizmos.DrawSphere(desiredPosition, 2f);
        if (humansInRange.Count > 0)
        {
            Gizmos.color = Color.grey;
            for (int i = 0; i < humansInRange.Count; i++)
            {
                Gizmos.DrawLine(transform.position, humansInRange[i].transform.position);
            }

            if (closestHuman != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, closestHuman.transform.position);
            }
        }
    }
}
