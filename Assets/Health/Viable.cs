using System;
using UnityEngine;

public class Viable : Capable
{
    [SerializeField] private int _health;
    public int Health { set => _health = value; get => _health; }
    public event EventHandler<int> HealthChanged;
    public event EventHandler Dead;
    
    /// <summary>
    /// Returns true, if the damage resulted in death
    /// </summary>
    public virtual bool Damage()
    {
        ApplyModifications();
        _health--;
        HealthChanged?.Invoke(this, _health);
        if (_health == 0)
        {
            Dead?.Invoke(this, null);
            return true;
        }
        return false;
    }
}
