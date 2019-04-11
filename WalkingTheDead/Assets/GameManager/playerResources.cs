using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI numberOfZombiesUI;
    LayerMask groundLayerMask;
    ParticleSystem click;
    LoadSceneOnClick sceneLoader;
    PlayerMovement necromancer;

    public ParticleSystem clickSystemEffect;

    Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = maxHealth;
        hungerValue = maxHunger;
        baseHungerDecrement = -0.001f;

        healthBar.value = CalculateHealth();
        hungerBar.value = CalculateHunger();
        numberOFZombies = 3;
        click = Instantiate(clickSystemEffect, Vector3.zero, Quaternion.Euler(90.0f, 0.0f, 0.0f));

        groundLayerMask = LayerMask.GetMask("Ground");
        sceneLoader = FindObjectOfType<LoadSceneOnClick>();
        necromancer = FindObjectOfType<PlayerMovement>();

        particleEffectActive = false;
        mainCamera = GameObject.Find("PlayerCharacter/Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        hungerValue += (baseHungerDecrement * numberOFZombies);

        hungerBar.value = CalculateHunger();

        healthBar.value = CalculateHealth();

        if (Input.GetMouseButtonDown(0))
        {
            PlayParticleEffect();
        }

        numberOfZombiesUI.text = numberOFZombies.ToString();
    }

    public void DecreaseHungerLevel()
    {
        // Increase fed value
        hungerValue += 2.0f;

        // Keep hunger capped at max
        if (hungerValue > maxHunger)
            hungerValue = maxHunger;
    }

    public void DecreaseHealth()
    {
        // Decrease Health
        playerHealth -= 10.0f;

        // If Dead Load Lose Screen
        if (playerHealth < 0.0f)
        {
            sceneLoader.LoadLoseScreen();
        }
    }

    public void SpawnSoldiersAroundPlayer()
    {
        // Find 10 positions around Player
        // Instantiate 10 soldiers around him
        // Set these soldier's destination to go against the player
    }

    void PlayParticleEffect()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100.0f, groundLayerMask))
        {
                click.transform.position = hitInfo.point;
                click.Play();
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