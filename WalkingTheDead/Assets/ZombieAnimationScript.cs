using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationScript : MonoBehaviour
{
    Zombie parentScript;
    Scanner parentScanner;

    private void Awake()
    {
        parentScript = GetComponentInParent<Zombie>();
        parentScanner = parentScript.HumanScanner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KillClosestHuman()
    {
        GameObject toKill = parentScanner.GetClosestTargetInRange();

        if (toKill != null && Mathf.Abs(Vector3.Distance(parentScript.transform.position, toKill.transform.position)) < parentScript.Settings.AttackRange)
        {
            parentScanner.ObjectsInRange.Remove(toKill);
            Destroy(toKill);
            toKill = null;
        }
    }
}
