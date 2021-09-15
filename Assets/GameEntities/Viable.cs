using System;
using UnityEngine;

public abstract class Viable : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    protected EventArgs _deadEventArgs;
    private int _currentHealth;
    public event EventHandler<int> HealthChanged;
    public event EventHandler<EventArgs> Dead;
    
    /// <summary>
    /// Returns true, if the damage resulted in death
    /// </summary>
    public bool Damage()
    {
        _currentHealth--;
        HealthChanged?.Invoke(this, _currentHealth);
        if (_currentHealth == 0)
        {
            OnDie();
            return true;
        }
        return false;
    }
    protected virtual void OnDie()
    {
        Dead?.Invoke(this, _deadEventArgs);
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        _currentHealth = _initialHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Viable>() is var viable && viable != null)
            OnViableCollided(viable, collision);
    }
    protected abstract void OnViableCollided(Viable otherViable, Collision collision);
}
