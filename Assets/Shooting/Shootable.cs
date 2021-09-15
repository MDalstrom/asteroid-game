using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : Capable
{
    [SerializeField] private Vector3 _bulletSpawnOffset;
    [SerializeField] private Color _bulletColor;
    [SerializeField] private float _shootingCooldown = 0.3333f;
    private float _lastShotTime;
    
    public void Shoot()
    {
        ApplyModifications();
        if ((Time.time - _lastShotTime) > _shootingCooldown)
        {
            BulletsPool.Instance.Shoot(new BulletConfigurationDTO
            {
                Color = _bulletColor,
                Position = transform.TransformDirection(_bulletSpawnOffset).normalized + transform.position,
                Direction = transform.up,
                Source = gameObject
            });
            _lastShotTime = Time.time;
        }
    }
}
