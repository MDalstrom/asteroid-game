using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static List<MonoBehaviour> _instances;
    public static T GetInstance<T>() where T : MonoBehaviour
    {
        return _instances.First(x => x.GetType() == typeof(T)) as T;
    }
    
    [SerializeField] private MonoBehaviour _target;
    private void Awake()
    {
        if (_instances == null)
            _instances = new List<MonoBehaviour>();
        _instances.Add(_target);
    }
}