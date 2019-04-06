using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    List<GameObject> objectsInRange = new List<GameObject>();
    string tagToScanFor = "";
    SphereCollider scanArea;
    [SerializeField] float scanSize = 5.0f;
    Vector3 lastKnownObjectLocation;

    public Vector3 LastKnownObjectLocation { get => lastKnownObjectLocation; }
    public List<GameObject> ObjectsInRange { get => objectsInRange; }
    public string TagToScanFor { get => tagToScanFor; }

    public void SetupScanner(string tagToScanFor, float radius)
    {
        this.tagToScanFor = tagToScanFor;
        scanSize = radius;
        scanArea.radius = scanSize;
    }

    private void Awake()
    {
        scanArea = gameObject.AddComponent<SphereCollider>();
        scanArea.isTrigger = true;

    }

    public GameObject GetClosestTargetInRange()
    {
        // If there are no zombies in range, return nothing
        if (objectsInRange.Count == 0)
            return null;

        GameObject closestObject = objectsInRange[0];

        // Find The closest zombie out of the zombies that are in range
        foreach(GameObject zombie in objectsInRange)
        {
            if (Vector3.Distance(zombie.transform.position, transform.position) < 
                Vector3.Distance(closestObject.transform.position, transform.position))
            {
                // Store it as the closest Zombie
                closestObject = zombie;
            }
        }

        return closestObject;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(tagToScanFor))
        {
            // Store it as the last known object
            if (objectsInRange.Count == 0)
            {
                lastKnownObjectLocation = other.gameObject.transform.position;
            }

            objectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToScanFor))
        {
            // Store it as the last known object
            if (objectsInRange.Count == 1)
            {
                lastKnownObjectLocation = other.gameObject.transform.position;
            }
                
            objectsInRange.Remove(other.gameObject);

            
        }
    }

    private void OnDrawGizmos()
    {
        
        // Make the sphere yellow when there are zombies around
        if (objectsInRange.Count > 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(transform.position, scanSize);
    }
}
