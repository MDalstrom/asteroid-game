using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(ScoreHolder))]
public class Ship : Viable
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _acceleration;
    private Vector3 _movingDirection;

    /// i know it's said there's no friction, but (for me) game seems unplayable without it
    /// in addition, origin has friction
    [SerializeField] private bool _needFriction;
    [SerializeField] private float _frictionalDeceleration;

    [SerializeField] private float _shootingCooldown = 0.3333f;
    /// just looks cool and was easy to make
    [SerializeField] private bool _needKnockback;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private Vector3 _bulletSpawnOffset;
    private float _lastShotTime;

    private Renderer _renderer;
    private Color _defaultColor;
    private bool _isInvincible = true;
    [SerializeField] private float _blinkingFrequency = 0.5f;

    private ScoreHolder _scoreHolder;

    public void Shoot()
    {
        if ((Time.time - _lastShotTime) > _shootingCooldown)
        {
            BulletsPool.Instance.Shoot(new BulletConfigurationDTO
            {
                Color = Color.green,
                Position = transform.TransformDirection(_bulletSpawnOffset).normalized + transform.position,
                Direction = transform.up,
                Source = _scoreHolder
            });
            _lastShotTime = Time.time;
        }

        if (_needKnockback)
            _movingDirection -= transform.up * _knockbackForce;
    }
    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.back * Mathf.Clamp(angle, -_rotatingSpeed, _rotatingSpeed));
    }
    public void AddAcceleration()
    {
        _movingDirection += transform.up * _acceleration;
        _movingDirection = Vector3.ClampMagnitude(_movingDirection, _maxSpeed);
    }
    public override bool Damage()
    {
        if (_isInvincible)
            return false;
        else
            return base.Damage();
    }
    protected override void OnViableCollided(Viable otherViable, Collision collision)
    {
        Damage();
        _movingDirection -= (collision.contacts[0].point - transform.position) * _knockbackForce;
    }

    protected override void Start()
    {
        base.Start();
        _scoreHolder = GetComponent<ScoreHolder>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }
    private void FixedUpdate()
    {
        transform.position += _movingDirection;
        if (_needFriction)
            _movingDirection -= _movingDirection.normalized * _frictionalDeceleration;

        if (_isInvincible)
        {
            _renderer.material.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0f);
            if (Time.time % _blinkingFrequency > _blinkingFrequency / 2f)
                _renderer.material.color = _defaultColor;
            else
                _renderer.material.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0f);
        }
    }
}

[CustomEditor(typeof(Ship))]
[CanEditMultipleObjects]
public class ShipEditor : Editor
{
    private Ship _ship;

    private SerializedProperty _initialHealth;

    private SerializedProperty _maxSpeed;
    private SerializedProperty _rotatingSpeed;
    private SerializedProperty _acceleration;

    private SerializedProperty _needFriction;
    private SerializedProperty _frictionalDeceleration;

    private SerializedProperty _shootingCooldown;
    private SerializedProperty _bulletSpawnOffset;
    private SerializedProperty _needKnockback;
    private SerializedProperty _knockbackForce;

    private void OnEnable()
    {
        _ship = target as Ship;

        _initialHealth = serializedObject.FindProperty("_initialHealth");

        _maxSpeed = serializedObject.FindProperty("_maxSpeed");
        _rotatingSpeed = serializedObject.FindProperty("_rotatingSpeed");
        _acceleration = serializedObject.FindProperty("_acceleration");

        _needFriction = serializedObject.FindProperty("_needFriction");
        _frictionalDeceleration = serializedObject.FindProperty("_frictionalDeceleration");

        _shootingCooldown = serializedObject.FindProperty("_shootingCooldown");
        _bulletSpawnOffset = serializedObject.FindProperty("_bulletSpawnOffset");
        _needKnockback = serializedObject.FindProperty("_needKnockback");
        _knockbackForce = serializedObject.FindProperty("_knockbackForce");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ///

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_initialHealth);

        ///

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Moving", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_maxSpeed);
        if (_maxSpeed.floatValue < 0)
            _maxSpeed.floatValue = 0;
        EditorGUILayout.PropertyField(_rotatingSpeed);
        if (_rotatingSpeed.floatValue < 0)
            _rotatingSpeed.floatValue = 0;
        EditorGUILayout.PropertyField(_acceleration);
        if (_acceleration.floatValue < 0 || _acceleration.floatValue > _maxSpeed.floatValue)
            _acceleration.floatValue = Mathf.Clamp(_acceleration.floatValue, 0, _maxSpeed.floatValue);

        ///

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Friction", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_needFriction);
        if (_needFriction.boolValue)
        {
            EditorGUILayout.PropertyField(_frictionalDeceleration);
            if (_frictionalDeceleration.floatValue < 0)
                _frictionalDeceleration.floatValue = 0;
        }

        ///

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Shooting", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(_shootingCooldown);
        if (_shootingCooldown.floatValue < 0)
            _shootingCooldown.floatValue = 0;

        var value = EditorGUILayout.Vector2Field("Bullet Spawn Offset", _bulletSpawnOffset.vector3Value);
        _bulletSpawnOffset.vector3Value = new Vector3(value.x, value.y, _ship.transform.position.z);

        EditorGUILayout.PropertyField(_needKnockback);
        if (_needKnockback.boolValue)
        {
            EditorGUILayout.PropertyField(_knockbackForce);
            if (_knockbackForce.floatValue < 0)
                _knockbackForce.floatValue = 0;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
