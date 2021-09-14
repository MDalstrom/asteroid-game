using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    [Header("Spawn settings")]
    [SerializeField] private bool _needSpawnOnWidth;
    [SerializeField] [Range(0f, 1f)] private float _widthPercentage;
    [SerializeField] private bool _needSpawnOnHeight;
    [SerializeField] [Range(0f, 1f)] private float _heightPercentage;
    private float _maxHeight;
    private float _maxWidth;

    protected virtual void Start()
    {
        var camera = Camera.main;
        _maxHeight = camera.orthographicSize;
        _maxWidth = Screen.width * camera.orthographicSize / Screen.height;
    }
    protected virtual T Spawn()
    {
        var targetObject = Instantiate(_prefab);
        var target = targetObject.GetComponent<T>();

        targetObject.transform.position = GetRandomPos();

        return target;
    }

    private Vector2 GetRandomPos()
    {
        if (_needSpawnOnWidth && IsProbably(0.5f))
        {
            return new Vector2(
                Random.Range(-_maxWidth, _maxWidth),
                _maxHeight * RollPlusOrMinus()
                );
        }
        else if (_needSpawnOnHeight && IsProbably(0.5f))
        {
            return new Vector2(
                _maxWidth * RollPlusOrMinus(),
                Random.Range(-_maxHeight, _maxHeight)
                );
        }
        else
            return GetCustomPos();
    }
    protected virtual Vector2 GetCustomPos() => throw new System.Exception($"Choose at least one spawn option of the spawner {this}");

    private static int RollPlusOrMinus()
    {
        return IsProbably(0.5f) ? (-1) : 1;
    }
    private static bool IsProbably(float chance)
    {
        return Random.Range(0f, 1f) > chance;
    }
}
