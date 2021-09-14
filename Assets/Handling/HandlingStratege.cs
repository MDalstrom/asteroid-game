using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandlingStratege
{
    event Action ShootingPressed;
    event Action MovingPressed;
    event EventHandler<bool> RotationPressed;

    void Update(GameObject context);
}
