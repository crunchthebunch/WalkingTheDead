using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HumanAI/Settings")]
public class HumanSettings : ScriptableObject
{
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float walkRadius = 10f;
    [SerializeField] float wanderDelay = 2.0f;
    [SerializeField] float fleeSpeed = 5.0f;
    [SerializeField] float vision = 5f;

    public float MovementSpeed { get => movementSpeed; }
    public float WalkRadius { get => walkRadius; }
    public float WanderDelay { get => wanderDelay; }
    public float FleeSpeed { get => fleeSpeed; }
    public float Vision { get => vision;  }
}
