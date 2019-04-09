using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerResources : MonoBehaviour
{
    public float playerHealth;
    public float hungerValue;
    public float baseHungerIncrement;
    public float maxHealth = 100.0f;
    public float maxHunger = 100.0f;
    public float numberOFZombies;

    public Slider healthBar;
    public Slider hungerBar;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = maxHealth;
        hungerValue = 0.0f;
        baseHungerIncrement = 0.0001f;

        healthBar.value = CalculateHealth();
        hungerBar.value = CalculateHunger();
        numberOFZombies = 3;
    }

    // Update is called once per frame
    void Update()
    {
        hungerValue += baseHungerIncrement;

        hungerBar.value = CalculateHunger();

        healthBar.value = CalculateHealth();
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
