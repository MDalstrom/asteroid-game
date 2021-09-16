using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public abstract class Configuration { }
    public abstract void Configure(Configuration config);
    public abstract void Spawn();
    public abstract void Despawn();
}
