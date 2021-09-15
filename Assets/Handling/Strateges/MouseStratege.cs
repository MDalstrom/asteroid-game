using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStratege : IHandlingStratege
{
    public MouseStratege()
    {
        _camera = Camera.main;
    }

    public event Action ShootingPressed;
    public event Action MovingPressed;
    public event EventHandler<float> RotationPressed;
    private Camera _camera;

    public void Update(GameObject context)
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
            ShootingPressed?.Invoke();

        context.transform.LookAt(_camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetMouseButton(1))
            MovingPressed?.Invoke();
    }
}
