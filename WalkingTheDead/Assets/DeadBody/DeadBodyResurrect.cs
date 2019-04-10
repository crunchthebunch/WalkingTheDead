﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyResurrect : MonoBehaviour
{
    [SerializeField] GameObject zombieSpawn = null;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "PlayerCharacter")
        {
            print("PLAYER DETECTED");
            if (Input.GetKeyDown("e"))
            {
                Instantiate(zombieSpawn, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
