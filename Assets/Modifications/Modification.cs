using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModification
{
    void Modify();
}
public abstract class Modification<T> : MonoBehaviour, IModification
{
    public abstract void Modify();
}
