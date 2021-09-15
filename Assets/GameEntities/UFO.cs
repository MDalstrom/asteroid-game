using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Viable
{
    [SerializeField] private float _speed;
    private Vector3 _movementDirection;
    protected override void Start()
    {
        base.Start();
        _movementDirection = Vector2.right * _speed * ((Random.Range(0f, 1f) > 0.5f) ? 1 : (-1));
    }
    private void FixedUpdate()
    {
        transform.position += _movementDirection;
    }
    protected override void OnCollisionEnter(Collision collision)
    {
    }
}