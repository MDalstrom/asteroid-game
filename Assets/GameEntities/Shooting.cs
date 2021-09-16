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
            Singleton.GetInstance<BulletsPool>().Spawn(new Bullet.Configuration { 
                Color = _bulletColor,
                Position = transform.position + transform.TransformDirection(_bulletSpawnOffset),
                Direction = direction
            });
            _lastShotTime = Time.time;
        }
    }
}
