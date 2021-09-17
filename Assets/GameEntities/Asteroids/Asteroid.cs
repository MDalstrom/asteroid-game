using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moving))]
public class Asteroid : PoolObject
{
    public class Configuration : PoolObjectConfiguration
    {
        public bool IsParticle { get; set; }
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }
    }
    [Header("Moving")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [Header("Particles")]
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private int _particlesCount;
    [SerializeField] private float _particlesSpreadingAngle;
    private List<Asteroid> _particles;
    private int _particlesRemained;

    public override void Spawn(PoolObjectConfiguration baseConfig)
    {
        gameObject.SetActive(true);
        var config = baseConfig as Asteroid.Configuration;
        var moving = GetComponent<Moving>();
        if (config.IsParticle)
        {
            moving.SetSpeed(config.Speed);
            moving.SetDirection(config.Direction);
        }
        else
        {
            moving.SetSpeed(GetRandomSpeed());
            moving.SetDirection(UnityEngine.Random.insideUnitCircle);
            SpawnParticles();
        }
        _particlesRemained = _particlesCount;
    }
    public override void Despawn()
    {
        if (_particlesCount == 0)
            base.Despawn();
        else
        {
            gameObject.SetActive(false);
            _particles.ForEach(x =>
            {
                x.Spawn(new Asteroid.Configuration
                {
                    IsParticle = true,
                    Speed = GetRandomSpeed(),
                    Direction = Quaternion.AngleAxis(_particlesSpreadingAngle, Vector3.forward) * transform.up
                });
            });
        }
    }

    private float GetRandomSpeed()
    {
        return UnityEngine.Random.Range(_minSpeed, _maxSpeed);
    }
    private void SpawnParticles()
    {
        if (_particlesCount == 0)
            return;

        _particles = new List<Asteroid>();
        for (int i = 0; i < _particlesCount; i++)
        {
            var particle = Instantiate(_particlePrefab, transform.parent);
            particle.transform.position = transform.position;

            var particleComponent = particle.GetComponent<Asteroid>();
            particleComponent.Despawned += OnParticleDispawned;
            particleComponent.Spawn(new Asteroid.Configuration 
            { 
                IsNew = true,
                IsParticle = false
            });
            _particles.Add(particleComponent);

            particle.SetActive(false);
        }
    }
    private void OnParticleDispawned(object sender, EventArgs args)
    {
        _particlesRemained--;
        if (gameObject.activeInHierarchy == false && _particlesRemained == 0)
            base.Despawn();
    }
}
