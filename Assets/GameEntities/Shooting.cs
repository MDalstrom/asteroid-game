using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Color _bulletColor;
    [SerializeField] private float _shootingCooldown = 0.3333f;
    [SerializeField] private Vector3 _bulletSpawnOffset;
    private float _lastShotTime;

    public void Shoot(Vector2 direction)
    {
        if ((Time.time - _lastShotTime) > _shootingCooldown)
        {
            BulletsPool.Instance.Shoot(new BulletConfigurationDTO
            {
                Color = _bulletColor,
                Position = transform.TransformDirection(_bulletSpawnOffset).normalized + transform.position,
                Direction = direction,
                Source = gameObject
            });
            _lastShotTime = Time.time;
        }
    }
}
