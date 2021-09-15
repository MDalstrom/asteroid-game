using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rock : Viable
{
    [Header("Particles")]
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] [Min(0)] private int _particlesCount;
    [SerializeField] [Min(0)] private int _spreadingAngle;
    [Header("Speed")]
    [SerializeField] [Min(0)] private float _minSpeed;
    [SerializeField] [Min(0)] private float _maxSpeed;
    private float _speed;
    private Vector3 FlightDirection{ set => transform.up = value; }

    public void SelfConfigure()
    {
        FlightDirection = UnityEngine.Random.insideUnitCircle * _speed;
        _speed = GetRandomSpeed();
    }
    protected override void OnDie()
    {
        if (_particlePrefab != null)
        {
            var mutualSpeed = GetRandomSpeed();
            var particles = new Rock[_particlesCount];
            for (int i = 0; i < _particlesCount; i++)
                particles[i].CreateParticle(mutualSpeed, _spreadingAngle * ((i % 2 == 0) ? 1 : (-1)));
            _deadEventArgs = new DeadEventArgs { Particles = particles };
        }
        base.OnDie();
    }
    private float GetRandomSpeed()
    {
        return UnityEngine.Random.Range(_minSpeed, _maxSpeed);
    }
    private Rock CreateParticle(float speed, float angle)
    {
        var rock = Instantiate(_particlePrefab).GetComponent<Rock>();
        rock._speed = speed;
        rock.transform.position = transform.position;
        rock.FlightDirection = Quaternion.AngleAxis(angle, Vector3.forward) * transform.up;
        return rock;
    }
    public class DeadEventArgs : EventArgs
    {
        public Rock[] Particles { get; set; }
    }
}