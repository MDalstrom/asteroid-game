using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moving))]
[RequireComponent(typeof(Shooting))]
public class UFOAI : PoolObject
{
    private void Start()
    {
        GetComponent<Moving>().SetDirection(Vector2.right * ((Random.Range(0f, 1f) > 0.5f) ? 1 : (-1)));
    }

    public override void Configure(PoolObjectConfiguration config)
    {

    }
}
