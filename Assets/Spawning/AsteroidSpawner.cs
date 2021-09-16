using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AsteroidSpawner : Spawner<Asteroid>
{
    [SerializeField] private int _initialWave;
    private int _currentWave;
    private int _rocksRemained;

    public override Asteroid Spawn(PoolObjectConfiguration config)
    {
        var spawned = base.Spawn(config);
        if (config.IsNew)
            spawned.Despawned += OnAsteroidDespawned;
        return spawned;
    }
    private void SpawnNewWave()
    {
        _currentWave++;
        _rocksRemained = _currentWave * 7;
    }
    private void OnAsteroidDespawned(object sender, EventArgs e)
    {
        _rocksRemained--;
        if (_rocksRemained == 0)
            SpawnNewWave();
    }
    private void Start()
    {
        Debug.Log(Enumerable.Repeat(UnityEngine.Random.Range(0, 10), 10));
    }
}
