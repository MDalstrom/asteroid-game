using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Ship _player;
    private IHandlingStratege _stratege;

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

    private void OnMovingPressed(object sender, bool isPositive)
    {
        _player.AddAcceleration(isPositive);
    }
    private void OnRotationPressed(object sender, bool isPositive)
    {
        _player.Rotate(isPositive);
    }
    private void OnShootingPressed()
    {
        _player.Shoot();
    }

    private void Update()
    {
        _stratege.Update(_player.gameObject);
    }

}
