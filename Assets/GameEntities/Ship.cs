using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _acceleration;

    private Vector3 _movingDirection;

    public void Shoot()
    {

    }
    public void Rotate(bool positive)
    {
        transform.Rotate(Vector3.one * _rotatingSpeed * (positive ? 1 : -1));
    }
    public void AddAcceleration()
    {
        _movingDirection += transform.up * _acceleration;
        _movingDirection = Vector3.ClampMagnitude(_movingDirection, _maxSpeed);
    }

    private void Update()
    {
        transform.position += _movingDirection;
    }
}
