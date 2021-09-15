using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class RockSpawner : Spawner<Rock>
{
    [SerializeField] [Min(1)] private int _initialCount = 2;
}
