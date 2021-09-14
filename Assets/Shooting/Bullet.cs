using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletConfigurationDTO
{
    public Vector2 Position;
    public Vector2 Direction;
    public Color Color;
}
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed; 

    public event EventHandler Deinitialized;
    public void Initialize(BulletConfigurationDTO config)
    {
        gameObject.SetActive(true);

        GetComponent<MeshRenderer>().material.color = config.Color;
        transform.position = config.Position;
        transform.up = config.Direction;
    }
    public void Deinitialize()
    {
        gameObject.SetActive(false);
        Deinitialized?.Invoke(this, null);
    }

    private void Update()
    {
        transform.position += transform.up * _speed;
    }
}
