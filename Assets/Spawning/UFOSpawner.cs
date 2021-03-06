using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UFOSpawner : Spawner<UFOAI>
{
    [Header("Timing Settings")]
    [SerializeField] [Min(0)] private float _maxCooldown = 40f;
    [SerializeField] [Min(0)] private float _minCooldown = 20f;
    private float _nextCooldown;
    private float _lastMeasuredTime;

    public override UFOAI Spawn(PoolObjectConfiguration config)
    {
        var ufo = base.Spawn(config);
        if (config.IsNew)
            ufo.Despawned += (s, e) => _lastMeasuredTime = Time.time;
        return ufo;
    }
    private void GenerateCooldown()
    {
        _nextCooldown = Random.Range(_minCooldown, _maxCooldown);
    }
    protected override void Awake()
    {
        base.Awake();
        GenerateCooldown();
    }
    private void FixedUpdate()
    {
        if (Time.time - _lastMeasuredTime > _nextCooldown)
        {
            Spawn(new PoolObjectConfiguration());
            _lastMeasuredTime = float.PositiveInfinity;
        }
    }
}
