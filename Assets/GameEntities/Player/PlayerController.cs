using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Viable))]
[RequireComponent(typeof(InvincibleViability))]
[RequireComponent(typeof(Shootable))]
public class PlayerController : MonoBehaviour
{
    private Movable _movable;
    private Shootable _shootable;
    private IHandlingStratege _stratege;

    #region Handling
    public void ChangeHandlingStratege(string strategeName)
    {
        _stratege = Activator.CreateInstance(Type.GetType(strategeName)) as IHandlingStratege;
    }
    public void ChangeHandlingStratege(IHandlingStratege newStratege)
    {
        _stratege = newStratege;
        _stratege.MovingPressed += OnMovingPressed;
        _stratege.RotationPressed += OnRotationPressed;
        _stratege.ShootingPressed += OnShootingPressed;
    }
    private void OnMovingPressed()
    {
        _movable.AddAcceleration();
    }
    private void OnRotationPressed(object sender, float angle)
    {
        _movable.Rotate(angle);
    }
    private void OnShootingPressed()
    {
        _shootable.Shoot();
    }
    #endregion

    private void Start()
    {
        _movable = GetComponent<Movable>();
        _shootable = GetComponent<Shootable>();

        ChangeHandlingStratege(new MouseStratege());
    }
    private void Update()
    {
        _stratege.Update(gameObject);
    }

}
