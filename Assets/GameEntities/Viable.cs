using System;
using UnityEngine;

public abstract class Viable : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    protected EventArgs _deadEventArgs;
    public int Health { get; protected set; }
    public event EventHandler<EventArgs> Dead;
    protected virtual void Start()
    {
        Health = _initialHealth;
    }
    public void Damage()
    {
        Health--;
        if (Health == 0)
            OnDie();
    }
    protected virtual void OnDie()
    {
        Dead?.Invoke(this, _deadEventArgs);
        Destroy(gameObject);
    }
}
