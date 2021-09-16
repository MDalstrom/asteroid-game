using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Moving))]
public class Bullet : SelfDestroyingPoolObject
{
    public class Configuration : PoolObjectConfiguration
    {
        public Color Color;
        public Vector3 Position;
        public Vector3 Direction;
    }

    [SerializeField] private float _speed; 
    private Moving _moving;

    public override void Configure(PoolObjectConfiguration commonConfig)
    {
        var config = commonConfig as Configuration;
        transform.position = config.Position;
        _moving.MovingDirection = config.Direction * _speed;
    }

    private void Awake()
    {
        _moving = GetComponent<Moving>();
        _lifetime = Screen.width * Camera.main.orthographicSize / Screen.height / _speed * Time.fixedDeltaTime * 2;
    }
}
