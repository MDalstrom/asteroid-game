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
        public Color Color { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
    }

    [SerializeField] private float _speed; 
    private Moving _moving;
    private Renderer _renderer;

    public override void Configure(PoolObjectConfiguration commonConfig)
    {
        var config = commonConfig as Configuration;
        transform.position = config.Position;
        _moving.MovingDirection = config.Direction * _speed;
        _renderer.material.color = config.Color;
    }

    private void Awake()
    {
        _moving = GetComponent<Moving>();
        _renderer = GetComponent<Renderer>();
        _lifetime = Screen.width * Camera.main.orthographicSize / Screen.height / _speed * Time.fixedDeltaTime * 2;
    }
}
