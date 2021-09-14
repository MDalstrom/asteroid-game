using System;
using UnityEngine;

public abstract class Viable : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    public int Health { get; protected set; }
    public event EventHandler Dead;
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
        Dead?.Invoke(this, null);
    }
}
