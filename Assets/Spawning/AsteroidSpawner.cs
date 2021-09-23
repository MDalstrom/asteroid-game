using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AsteroidSpawner : Spawner<AsteroidAI>
{
    [SerializeField] private int _initialWave = 2;
    private int _currentWave;
    private int _rocksRemained;

    public override AsteroidAI Spawn(PoolObjectConfiguration config)
    {
        var spawned = base.Spawn(config);
        if (config.IsNew)
            spawned.Despawned += OnAsteroidDespawned;
        return spawned;
    }
    private void SpawnWave()
    {
        _currentWave++;
        _rocksRemained = _currentWave;
        for (int i = 0; i < _currentWave; i++)
            Spawn(new AsteroidAI.Configuration
            {
                IsParticle = false,
                IsNew = true
            });
    }
    private void OnAsteroidDespawned(object sender, EventArgs e)
    {
        _rocksRemained--;
        if (_rocksRemained == 0)
            SpawnWave();
    }

    protected override void Awake()
    {
        base.Awake();
        _currentWave = _initialWave;
        SpawnWave();
    }
}
