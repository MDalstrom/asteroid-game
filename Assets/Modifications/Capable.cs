using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Capable : MonoBehaviour
{
    private List<Modification<Capable>> _modifications;
    protected void ApplyModifications()
    {
        _modifications.ForEach(x => x.Modify(this));
    }
    protected virtual void Start()
    {
        _modifications = GetComponents<Modification<Capable>>().Where(x => {
            var generics = x.GetType().GetGenericArguments();
            if (generics.Length == 1 && generics[0] == this.GetType())
                return true;
            else
                return false;
        }).ToList();
    }
}
