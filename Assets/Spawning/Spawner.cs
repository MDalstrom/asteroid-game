using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    [Header("Spawn Settings")]
    [SerializeField] private bool _needSpawnOnWidth;
    [SerializeField] [Range(0f, 1f)] private float _widthPercentage;
    [SerializeField] private bool _needSpawnOnHeight;
    [SerializeField] [Range(0f, 1f)] private float _heightPercentage;
    private float _maxHeight;
    private float _maxWidth;

    protected virtual void Awake()
    {
        var camera = Camera.main;
        _maxHeight = camera.orthographicSize * _heightPercentage;
        _maxWidth = Screen.width * camera.orthographicSize / Screen.height * _widthPercentage;
    }
    protected virtual T Spawn()
    {
        var targetObject = Instantiate(_prefab);
        targetObject.transform.position = GetRandomPos();

        var target = targetObject.GetComponent<T>();
        return target;
    }

    private Vector2 GetRandomPos()
    {
        if (_needSpawnOnHeight && _needSpawnOnWidth)
        {
            if (IsProbably(0.5f))
                return GetPosOnWidth();
            else 
                return GetPosOnHeight();
        }
        else if (_needSpawnOnWidth)
        {
            return GetPosOnWidth();
        }
        else if (_needSpawnOnHeight)
        {
            return GetPosOnHeight();
        }
        else
            return GetCustomPos();
    }
    private Vector2 GetPosOnHeight()
    {
        return new Vector2(
                _maxWidth * RollPlusOrMinus(),
                Random.Range(-_maxHeight, _maxHeight)
                );
    }
    private Vector2 GetPosOnWidth()
    {
        return new Vector2(
                    Random.Range(-_maxWidth, _maxWidth),
                    _maxHeight * RollPlusOrMinus()
                    );
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
