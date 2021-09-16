using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moving))]
[RequireComponent(typeof(Shooting))]
public class PlayerController : MonoBehaviour
{
    private IHandlingStratege _stratege;
    private Moving _moving;
    private Shooting _shooting;

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
        _moving.AddAcceleration();
    }
    private void OnRotationPressed(object sender, float angle)
    {
        _moving.Rotate(angle);
    }
    private void OnShootingPressed()
    {
        _shooting.Shoot(transform.up);
    }
    #endregion

    private void Start()
    {
        _moving = GetComponent<Moving>();
        _shooting = GetComponent<Shooting>();

        ChangeHandlingStratege(new MouseStratege());
    }
    private void Update()
    {
        _stratege.Update(gameObject);
    }

}
