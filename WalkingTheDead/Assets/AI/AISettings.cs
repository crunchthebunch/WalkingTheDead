using UnityEngine;

public class AISettings : ScriptableObject
{
    [SerializeField] protected float vision = 5.0f;
    [SerializeField] protected float movementSpeed = 2.0f;

    public float MovementSpeed { get => movementSpeed; }
    public float Vision { get => vision; }
}
