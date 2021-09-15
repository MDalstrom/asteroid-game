using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shootable))]
[RequireComponent(typeof(Movable))]
public class KnockableShooting : Modification<Shootable>
{
    [SerializeField] private float _knockbackForce;
    private Movable _movable;

    public override void Modify()
    {
        _movable.AddImpulse(_movable.MovingDirection * -_knockbackForce);
    }

    private void Start()
    {
        _movable = GetComponent<Movable>();
    }
}
