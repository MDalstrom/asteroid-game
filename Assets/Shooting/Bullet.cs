using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletConfigurationDTO
{
    public Vector2 Position;
    public Vector2 Direction;
    public Color Color;
    public ScoreHolder Source;
}
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _lifetime;
    private Coroutine _selfDestroying;
    private ScoreHolder _source;

    public event EventHandler Deinitialized;

    public void Initialize(BulletConfigurationDTO config)
    {
        gameObject.SetActive(true);
        _selfDestroying = StartCoroutine(SelfDestroyingRoutine());

        GetComponent<MeshRenderer>().material.color = config.Color;
        transform.position = config.Position;
        transform.up = config.Direction;
        _source = config.Source;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == _source)
            return;

        Deinitialize();
        if (collision.collider.GetComponent<Viable>() is var viable && viable != null)
        {
            if (viable.Damage())
                _source?.Transfer(collision.collider.GetComponent<ScoreHolder>());
        }
    }
}
