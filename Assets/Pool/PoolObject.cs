using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PoolObject : MonoBehaviour
{
    public virtual void Spawn(PoolObjectConfiguration config)
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
