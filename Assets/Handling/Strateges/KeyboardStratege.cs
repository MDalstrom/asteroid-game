using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardStratege : IHandlingStratege
{
    public event Action ShootingPressed;
    public event EventHandler<bool> MovingPressed;
    public event EventHandler<bool> RotationPressed;

    public void Update(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShootingPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
            MovingPressed?.Invoke(this, true);

        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
            MovingPressed?.Invoke(this, false);

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            RotationPressed?.Invoke(this, true);

        if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
            RotationPressed?.Invoke(this, false);
    }
}
