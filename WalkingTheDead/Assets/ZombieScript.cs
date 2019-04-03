using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    Vector3 desiredPosition;

    Transform player;
    NavMeshAgent agent;
    
    List<Collider> humansInRange = new List<Collider>();
    Collider closestHuman;

    SphereCollider detectionRange;
    CapsuleCollider attackRange;
    


    private void Start()
    {
        player = GameObject.Find("Fake Player").transform;
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GetComponent<SphereCollider>();
        attackRange = GetComponent<CapsuleCollider>();
        
    }

    private void Update()
    {

        //Set DesiredPosition
        SetDesiredPosition();

        //Move to Desired position
        agent.SetDestination(desiredPosition);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
        {
            humansInRange.Add(other);
            
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
                    break;
                }
            }
        }
    }

    void SetDesiredPosition()
    {

        if (humansInRange.Count > 0)
        {
            GetClosestHuman();
            desiredPosition = closestHuman.transform.position;
        }
        else desiredPosition = player.position;
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


    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color =  new Color(0,0,1, 0.1f);
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
