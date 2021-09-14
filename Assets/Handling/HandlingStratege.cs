using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveEventArgs : EventArgs
{
    public bool IsPositive { get; set; }
}
public interface IHandlingStratege
{
    event Action ShootingPressed;
    event EventHandler<PositiveEventArgs> MovingPressed;
    event EventHandler<PositiveEventArgs> RotationPressed;

    void Update(GameObject context);
}
