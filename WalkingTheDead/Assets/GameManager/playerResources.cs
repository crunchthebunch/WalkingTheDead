using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerResources : MonoBehaviour
{
    public float playerHealth = 100.0f;
    public float hungerValue = 0.0f;

    public float baseHungerIncrement = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hungerValue += baseHungerIncrement;
    }
}
