using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardStratege : IHandlingStratege
{
    public event Action ShootingPressed;
    public event Action MovingPressed;
    public event EventHandler<bool> RotationPressed;

    public void Update(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShootingPressed?.Invoke();

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
            MovingPressed?.Invoke();

        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
            RotationPressed?.Invoke(this, true);

        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
            RotationPressed?.Invoke(this, false);
    }
}
