﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyResurrect : MonoBehaviour
{
    PlayerResources gameManager;

    Animator anim;

    [SerializeField] GameObject playerObject = null;

    [SerializeField] GameObject zombieSpawn = null;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<PlayerResources>();
        playerObject = GameObject.Find("PlayerCharacter");
        anim = playerObject.GetComponentInChildren<Animator>();
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
                anim.SetBool("isResurrecting", true);

                Invoke("setAnimationFalse", 2.0f);
                Invoke("InstantiateZombie", 2.5f);

            }
        }
    }

    private void setAnimationFalse()
    {
        anim.SetBool("isResurrecting", false);
    }

    private void InstantiateZombie()
    {
        Instantiate(zombieSpawn, transform.position, transform.rotation);
        gameManager.numberOFZombies += 1;
        Destroy(gameObject);
    }
}
