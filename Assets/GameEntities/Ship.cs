using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ship : MonoBehaviour
{
    private float _maxSpeed;
    private float _rotatingSpeed;
    private float _acceleration;
    private bool _needFriction;
    private float _frictionalDecelerationRatio;
    private const float ShootingCooldown = 0.3333f;

    private float _lastShotTime;
    private Vector3 _movingDirection;

    public void Shoot()
    {
        if ((Time.time - _lastShotTime) > ShootingCooldown)
            BulletsPool.Instance.CreateBullet(transform.up.normalized);
    }
    public void Rotate(bool positive)
    {
        transform.Rotate(Vector3.back * _rotatingSpeed * (positive ? 1 : -1));
    }
    public void AddAcceleration()
    {
        _movingDirection += transform.up.normalized * _acceleration;
        _movingDirection = Vector3.ClampMagnitude(_movingDirection, _maxSpeed);
    }

    private void Update()
    {
        transform.position += _movingDirection;
    }
}

[CustomEditor(typeof(Ship))]
[CanEditMultipleObjects]
public class ShipEditor : Editor
{
    private SerializedProperty _maxSpeed;
    private void OnEnable()
    {
        serializedObject.FindProperty    
    }
}
