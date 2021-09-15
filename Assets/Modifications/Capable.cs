using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Capable : MonoBehaviour
{
    private List<IModification> _modifications;
    protected void ApplyModifications()
    {
        _modifications.ForEach(x => x.Modify());
    }
    protected virtual void Start()
    {
        _modifications = GetComponents<IModification>().Where(x => {
            var generics = x.GetType().GetGenericArguments();
            if (generics.Length == 1 && generics[0] == this.GetType())
                return true;
            else
                return false;
        }).ToList();
    }
}
