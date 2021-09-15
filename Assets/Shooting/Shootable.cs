using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField] private float _shootingCooldown = 0.3333f;
    [SerializeField] private bool _needKnockback;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private Vector3 _bulletSpawnOffset;
    private float _lastShotTime;

    public void Shoot()
    {
        if ((Time.time - _lastShotTime) > _shootingCooldown)
        {
            BulletsPool.Instance.Shoot(new BulletConfigurationDTO
            {
                Color = Color.green,
                Position = transform.TransformDirection(_bulletSpawnOffset).normalized + transform.position,
                Direction = transform.up,
                Source = _scoreHolder
            });
            _lastShotTime = Time.time;
        }

        if (_needKnockback)
            _movingDirection -= transform.up * _knockbackForce;
    }

    private void Start()
    {
        
    }
}
