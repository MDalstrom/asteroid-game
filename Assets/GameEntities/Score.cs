using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _worth;
    private int _wealth;
    public event EventHandler<int> WealthChanged;
    public void Transfer(Score from)
    {
        _wealth += from._worth;
        WealthChanged?.Invoke(this, _wealth);
    }
}
