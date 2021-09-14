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
    private float _lifetime;
    private Coroutine _selfDestroying;

    public event EventHandler Deinitialized;

    public void Initialize(BulletConfigurationDTO config)
    {
        gameObject.SetActive(true);
        _selfDestroying = StartCoroutine(SelfDestroyingRoutine());

        GetComponent<MeshRenderer>().material.color = config.Color;
        transform.position = config.Position;
        transform.up = config.Direction;
    }
    public void Deinitialize()
    {
        gameObject.SetActive(false);
        if (_selfDestroying != null)
        {
            StopCoroutine(_selfDestroying);
            _selfDestroying = null;
        }

        Deinitialized?.Invoke(this, null);
    }
    private IEnumerator SelfDestroyingRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Deinitialize();
    }
    private void OnEnable()
    {
        _lifetime = Screen.width * Camera.main.orthographicSize / Screen.height / _speed * Time.fixedDeltaTime * 2;
    }
    private void FixedUpdate()
    {
        transform.position += transform.up * _speed;
    }
}
