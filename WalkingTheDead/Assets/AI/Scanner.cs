using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // Modifyable
    [SerializeField] string tagToScanFor = "";
    [SerializeField] float scanSize = 5.0f;
    [SerializeField] Color triggeredColor = Color.yellow;
    [SerializeField] Color standardColor = Color.green;

    List<GameObject> objectsInRange = new List<GameObject>();
    SphereCollider scanArea;
    Vector3 lastKnownObjectLocation;


    public Vector3 LastKnownObjectLocation { get => lastKnownObjectLocation; }
    public List<GameObject> ObjectsInRange { get => objectsInRange; }
    public string TagsToScanFor { get => tagToScanFor; }

    public void SetupScanner(string tagToScanFor, float radius)
    {
        this.tagToScanFor = tagToScanFor;

        scanSize = radius;
        scanArea.radius = radius;
    }

    private void Awake()
    {
        scanArea = gameObject.AddComponent<SphereCollider>();

        scanArea.radius = scanSize;
        scanArea.isTrigger = true;

    }

    public GameObject GetClosestTargetInRange()
    {
        // If there are no zombies in range, return nothing
        if (objectsInRange.Count == 0)
            return null;

        GameObject closestObject = objectsInRange[0];

        // Find The closest zombie out of the zombies that are in range
        foreach(GameObject objectsInRange in objectsInRange)
        {
            // If the object no longer extist, remove  it from the list
            if (!objectsInRange)
            {
                this.objectsInRange.Remove(objectsInRange);

                // If we removed the last zombie, return null
                if (this.objectsInRange.Count == 0)
                    return null;

                continue;
            }

            if (Vector3.Distance(objectsInRange.transform.position, transform.position) < 
                Vector3.Distance(closestObject.transform.position, transform.position))
            {
                // Store it as the closest Zombie
                closestObject = objectsInRange;
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
