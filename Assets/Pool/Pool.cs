using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] protected GameObject _prefab;
    private List<T> _list;

    public virtual T Spawn(PoolObjectConfiguration config)
    {
        if (config == null)
            throw new System.Exception($"{GetType()}'s configuration cannot be null");

        var spawned = GetAvailableObject(out var isNew);
        config.IsNew = isNew;
        spawned.Configure(config);
        spawned.Spawn();
        return spawned;
    }

    private T GetAvailableObject(out bool isNew)
    {
        var first = _list.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
        isNew = false;
        if (first == null)
        {
            isNew = true;
            first = CreateNewObject();
        }
        return first;
    }
    protected T CreateNewObject()
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
