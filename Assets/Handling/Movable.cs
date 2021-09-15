using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _acceleration;

    [Header("Friction")]
    [SerializeField] private bool _needFriction;
    [SerializeField] private float _frictionalDeceleration;

    private Vector3 _movingDirection;
    public Vector3 MovingDirection => _movingDirection;

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.back * Mathf.Clamp(angle, -_rotatingSpeed, _rotatingSpeed));
    }
    public void AddAcceleration()
    {
        _movingDirection += transform.up * _acceleration;
        _movingDirection = Vector3.ClampMagnitude(_movingDirection, _maxSpeed);
    }
    public void AddImpulse(Vector3 impulse)
    {
        _movingDirection += impulse;
    }
    public void UpdatePosition()
    {
        transform.position += _movingDirection;
        if (_needFriction)
            _movingDirection -= _movingDirection.normalized * _frictionalDeceleration;
    }
}
