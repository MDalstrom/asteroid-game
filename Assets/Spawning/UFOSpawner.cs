using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UFOSpawner : Spawner<UFO>
{
    [SerializeField] [Min(0)] private float _maxCooldown = 40f;
    [SerializeField] [Min(0)] private float _minCooldown = 20f;
    private float _nextCooldown;
    private float _lastMeasuredTime;
    private void GenerateCooldown()
    {
        _nextCooldown = Random.Range(_minCooldown, _maxCooldown);
    }
    private void OnEnable()
    {
        GenerateCooldown();
    }
    private void Update()
    {
        if (Time.time - _lastMeasuredTime > _nextCooldown)
        {
            var ufo = Spawn();
            ufo.Dead += (s, e) => _lastMeasuredTime = Time.time;
            _lastMeasuredTime = float.PositiveInfinity;
        }
    }
}
