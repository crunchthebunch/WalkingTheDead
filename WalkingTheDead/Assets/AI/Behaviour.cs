﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Behaviour : MonoBehaviour
{
    public abstract void SetupComponent(AISettings settings);

    public abstract void DoBehaviour();
}
