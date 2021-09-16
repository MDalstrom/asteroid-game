using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modification<T> : MonoBehaviour where T : Capable
{
    public abstract void Modify(T sender);
}
