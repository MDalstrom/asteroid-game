using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStratege : IHandlingStratege
{
    public event Action ShootingPressed;
    public event Action MovingPressed;
    public event EventHandler<bool> RotationPressed;

    public void Update(GameObject context)
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
            ShootingPressed?.Invoke();

        /**/

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetMouseButton(1))
            MovingPressed?.Invoke();
    }
}
