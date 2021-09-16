using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Health _target;
    private List<GameObject> _hearts;

    private void Awake()
    {
        _hearts = new List<GameObject>();
        _target.ValueChanged += OnHealthChanged;
    }

    private void AddHeart()
    {
        _hearts.Add(Instantiate(_heartPrefab, transform));
    }
    private void RemoveHeart()
    {
        if (_hearts.Count == 0)
            return;
        var last = _hearts.Last();
        Destroy(last);
        _hearts.Remove(last);
    }
    private void OnHealthChanged(object sender, int e)
    {
        while (e > _hearts.Count)
            AddHeart();
        while (e < _hearts.Count)
            RemoveHeart();
    }
}
