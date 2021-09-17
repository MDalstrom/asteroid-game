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

    private Moving _moving;
    private Renderer _renderer;

    public override void Spawn(PoolObjectConfiguration baseConfig)
    {
        base.Spawn(baseConfig);

        var config = baseConfig as Configuration;
        transform.position = config.Position;
        _moving.SetDirection(config.Direction);
        _renderer.material.color = config.Color;
    }

    private void Awake()
    {
        _moving = GetComponent<Moving>();
        _renderer = GetComponent<Renderer>();
        _lifetime = Screen.width * Camera.main.orthographicSize / Screen.height / _moving.MaxSpeed * Time.fixedDeltaTime * 2;
    }
}
