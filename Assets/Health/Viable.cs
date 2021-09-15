using System;
using UnityEngine;

public class Viable : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    protected EventArgs _deadEventArgs;
    private int _currentHealth;
    public event EventHandler<int> HealthChanged;
    public event EventHandler<EventArgs> Dead;
    
    /// <summary>
    /// Returns true, if the damage resulted in death
    /// </summary>
    public virtual bool Damage()
    {
        _currentHealth--;
        HealthChanged?.Invoke(this, _currentHealth);
        if (_currentHealth == 0)
        {
            Dead?.Invoke(this, _deadEventArgs);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
    private void Start()
    {
        _currentHealth = _initialHealth;
    }
}
