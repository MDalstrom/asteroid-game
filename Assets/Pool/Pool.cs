using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolObjectConfiguration
{
    public bool IsNew { get; set; }
}
public abstract class Pool<T> : MonoBehaviour where T : PoolObject
{
    
    [SerializeField] protected GameObject _prefab;
    private List<T> _list;

    public virtual T Spawn(PoolObjectConfiguration config)
    {
        if (config == null)
            throw new System.Exception($"{GetType()}'s configuration cannot be null");
        var availableObject = GetAvailableObject(config);
        availableObject.Spawn(config);
        availableObject.Despawned += (s, e) => OnObjectDespawned(s as T);
        return availableObject;
    }
    public virtual void OnObjectDespawned(T sender) { }

    private T GetAvailableObject(PoolObjectConfiguration config)
    {
        var first = _list.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
        config.IsNew = false;
        if (first == null)
        {
            config.IsNew = true;
            first = CreateNewObject();
        }
        return first;
    }
    private T CreateNewObject()
    {
        var newObject = Instantiate(_prefab, transform);
        newObject.name = $"{typeof(T).Name} #{_list.Count + 1}";
        var component = newObject.GetComponent<T>();

        _list.Add(component);
        return component;
    }

    protected virtual void Awake()
    {
        _list = new List<T>();
    }
}
