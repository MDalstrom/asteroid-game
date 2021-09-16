using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _acceleration;

    [Header("Friction")]
    [SerializeField] private bool _needFriction;
    [SerializeField] private float _frictionalDeceleration;

    public Vector3 MovingDirection { get; set; }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.back * Mathf.Clamp(angle, -_rotatingSpeed, _rotatingSpeed));
    }
    public void SetDirection(Vector2 direction)
    {
        MovingDirection = direction * _maxSpeed;
    }
    public void AddAcceleration()
    {
        MovingDirection += transform.up * _acceleration;
        MovingDirection = Vector3.ClampMagnitude(MovingDirection, _maxSpeed);
    }
    public void AddImpulse(Vector3 impulse)
    {
        MovingDirection += impulse;
    }
    private void FixedUpdate()
    {
        transform.position += MovingDirection;
        if (_needFriction)
            MovingDirection -= MovingDirection.normalized * _frictionalDeceleration;
    }
}
