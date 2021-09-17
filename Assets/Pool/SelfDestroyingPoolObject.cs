using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelfDestroyingPoolObject : PoolObject
{
    [SerializeField] protected float _lifetime;
    private Coroutine _despawning;
    public override void Spawn(PoolObjectConfiguration config)
    {
        base.Spawn(config);
        _despawning = StartCoroutine(DespawningRoutine());
    }
    public override void Despawn()
    {
        base.Despawn();
        if (_despawning != null)
        {
            StopCoroutine(_despawning);
            _despawning = null;
        }
    }

    private IEnumerator DespawningRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Despawn();
    }
}
