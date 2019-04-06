using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerStateController : StateController
{
    VillagerSettings stats = null;
    Villager owner = null;

    public Villager Owner { get => owner; }
    public VillagerSettings Stats { get => stats; }

    private void Awake()
    {
        owner = GetComponent<Villager>();
        stats = owner.Settings;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
