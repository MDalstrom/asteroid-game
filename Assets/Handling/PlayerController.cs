using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Ship _player;
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

    private void OnMovingPressed()
    {
        _player.AddAcceleration();
    }
    private void OnRotationPressed(object sender, bool isPositive)
    {
        _player.Rotate(isPositive);
    }
    private void OnShootingPressed()
    {
        _player.Shoot();
    }

    private void Start()
    {
        ChangeHandlingStratege(new KeyboardStratege());
    }
    private void Update()
    {
        _stratege.Update(_player.gameObject);
    }

}
