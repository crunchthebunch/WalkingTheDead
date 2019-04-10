using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    public float playerHealth;
    public float hungerValue;
    public float baseHungerDecrement;
    public float maxHealth = 100.0f;
    public float maxHunger = 100.0f;
    public int numberOFZombies;

    public Slider healthBar;
    public Slider hungerBar;

    bool particleEffectActive;
    float particleEffectCounter;

    LayerMask groundLayerMask;

    ParticleSystem click;

    public ParticleSystem clickSystemEffect;

    Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = maxHealth;
        hungerValue = maxHunger;
        baseHungerDecrement = -0.005f;

        healthBar.value = CalculateHealth();
        hungerBar.value = CalculateHunger();
        numberOFZombies = 3;
        click = Instantiate(clickSystemEffect, Vector3.zero, Quaternion.Euler(90.0f, 0.0f, 0.0f));

        groundLayerMask = LayerMask.GetMask("Ground");

        particleEffectActive = false;
        mainCamera = GameObject.Find("PlayerCharacter/Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        hungerValue += baseHungerDecrement;

        hungerBar.value = CalculateHunger();

        healthBar.value = CalculateHealth();

        if (Input.GetMouseButtonDown(0))
        {
            PlayParticleEffect();
        }

        print(numberOFZombies);
    }

    void PlayParticleEffect()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100.0f, groundLayerMask))
        {
            //if (hitInfo.collider.tag == "Ground")
            //{
                click.transform.position = hitInfo.point;
                //clickSystemEffect.Play();
                click.Play();
                //Destroy(click, 1.1f);
            //}


        }
        else
        {

        }
    }

    float CalculateHealth()
    {
        return playerHealth / maxHealth;
    }

    float CalculateHunger()
    {
        return hungerValue / maxHunger;
    }
}
