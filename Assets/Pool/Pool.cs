using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] protected GameObject _prefab;
    private List<T> _list;

    public void Spawn(PoolObject.Configuration config)
    {
        var spawned = GetAvailableObject();
        spawned.Configure(config);
        spawned.Spawn();
    }

    private T GetAvailableObject()
    {
        var first = _list.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
        if (first == null)
            first = CreateNewObject();
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

    protected void Awake()
    {
        _list = new List<T>();
    }
}
