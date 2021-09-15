using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public sealed class RockSpawner : Spawner<Rock>
{
    [SerializeField] [Min(1)] private int _initialWaveSize = 2;
    [SerializeField] [Min(0)] private float _wavesDelay = 2f;
    private int _currentWaveSize;
    private int _rocksRemained;
    private void OnEnable()
    {
        _currentWaveSize = _initialWaveSize;
        SpawnWave();
    }
    private void SpawnWave()
    {
        _rocksRemained = _currentWaveSize * 7;
        for (int i = 0; i < _currentWaveSize; i++)
        {
            var rock = Spawn();
            rock.SelfConfigure();
            rock.Dead += OnRockDead;
        }
    }

    private void OnRockDead(object sender, EventArgs e)
    {
        (e as Rock.DeadEventArgs).Particles.ToList().ForEach(x => x.Dead += OnRockDead);
        (sender as Rock).Dead -= OnRockDead;
        
        _rocksRemained--;
        if (_rocksRemained == 0)
            OnWaveCompleted();
    }
    private void OnWaveCompleted()
    {
        _currentWaveSize++;
        Invoke("SpawnWave", _wavesDelay);
    }
}
