using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max;
    private int _value;

    public bool IsInvincible { get; set; }

    public event EventHandler<int> Damaged;
    public event EventHandler Died;

    /// <summary>
    /// Returns true, if the damage resulted in death
    /// </summary>
    public bool Damage()
    {
        if (IsInvincible)
            return false;
        _value--;
        Damaged?.Invoke(this, _value);
        if (_value == 0)
        {
            Died?.Invoke(this, null);
            return true;
        }
        return false;
    }
}
