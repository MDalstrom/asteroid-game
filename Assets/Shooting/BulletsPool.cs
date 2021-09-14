using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class BulletsPool : Singleton<BulletsPool>
{
    [Tooltip("Prefab must be saved in the deinitialized state")]
    [SerializeField] private GameObject _bulletPrefab;

    private ObservableCollection<Bullet> _pool;

    public void Shoot(BulletConfigurationDTO config)
    {
        if (_pool.FirstOrDefault(x => !x.gameObject.activeInHierarchy) is var bullet && bullet != null)
            bullet.Initialize(config);
        else
            CreateBullet().Initialize(config);
    }

    private Bullet CreateBullet()
    {
        var bulletObject = Instantiate(_bulletPrefab, transform);
        var bullet = bulletObject.GetComponent<Bullet>();
        bulletObject.name = $"Bullet #{_pool.Count}";

        _pool.Add(bullet);
        return bullet;
    }

    private void Start()
    {
        _pool = new ObservableCollection<Bullet>();
    }
}
