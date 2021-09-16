using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObjectConfiguration { }
public abstract class PoolObject : MonoBehaviour
{
    public abstract void Configure(PoolObjectConfiguration config);
    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }

    public event EventHandler Despawned;
    public virtual void Despawn()
    {
        gameObject.SetActive(false);
        Despawned?.Invoke(this, null);
    }
}
