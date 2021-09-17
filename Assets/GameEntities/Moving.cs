using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _acceleration;

    [Header("Friction")]
    [SerializeField] private bool _needFriction;
    [SerializeField] private float _frictionalDeceleration;

    public Vector3 _movingDirection { get; set; }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.back * Mathf.Clamp(angle, -_rotatingSpeed, _rotatingSpeed));
    }
    public void SetSpeed(float newSpeed)
    {
        _maxSpeed = newSpeed;
    }
    public void SetDirection(Vector2 direction)
    {
        _movingDirection = direction * _maxSpeed;
    }
    public void AddAcceleration()
    {
        _movingDirection += transform.up * _acceleration;
        _movingDirection = Vector3.ClampMagnitude(_movingDirection, _maxSpeed);
    }
    private void FixedUpdate()
    {
        transform.position += _movingDirection;
        if (_needFriction)
            _movingDirection -= _movingDirection.normalized * _frictionalDeceleration;
    }
}
