using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max;
    private int _value;

    public bool IsInvincible { get; set; }

    public event EventHandler<int> ValueChanged;
    public event EventHandler Died;

    public void Regen()
    {
        _value = _max;
        ValueChanged?.Invoke(this, _value);
    }
    /// <summary>
    /// Returns true, if the damage resulted in death
    /// </summary>
    public bool Damage()
    {
        if (IsInvincible)
            return false;
        _value--;
        ValueChanged?.Invoke(this, _value);
        if (_value == 0)
        {
            Died?.Invoke(this, null);
            return true;
        }
        return false;
    }
    private void Start()
    {
        Regen();
    }
}
