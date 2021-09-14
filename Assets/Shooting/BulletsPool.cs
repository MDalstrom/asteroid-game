using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BulletsPool : Singleton<BulletsPool>
{
    [Tooltip("Prefab must be saved in the deinitialized state")]
    [SerializeField] private GameObject _bulletPrefab;

    private ObservableCollection<Bullet> _pool;
    private int _activeBulletsCount;

    public void Shoot(BulletConfigurationDTO config)
    {
        if (_activeBulletsCount == _pool.Count)
            CreateBullet();
        ActivateBullet(config);
    }

    private void CreateBullet()
    {
        var bulletObject = Instantiate(_bulletPrefab);
        var bullet = bulletObject.GetComponent<Bullet>();

        bullet.Deinitialized += (s, e) => OnBulletDeinitialized(s as Bullet);

        _pool.Insert(0, bullet);
    }
    private void ActivateBullet(BulletConfigurationDTO config)
    {
        if (_pool.Count == 0)
            return;
        _pool[0].Initialize(config);
        _activeBulletsCount++;
    }
    private void OnBulletDeinitialized(Bullet sender)
    {
        MoveToStart(sender);
        _activeBulletsCount--;
    }
    private void MoveToStart(Bullet subject)
    {
        _pool.Move(_pool.IndexOf(subject), 0);
    }

    private void Start()
    {
        _pool = new ObservableCollection<Bullet>();
    }
}
