using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Viable _target;
    private List<GameObject> _hearts;

    private void Start()
    {
        _hearts = new List<GameObject>();
        _target.HealthChanged += OnHealthChanged;
    }

    private void AddHeart()
    {
        _hearts.Add(Instantiate(_heartPrefab, transform));
    }
    private void RemoveHeart()
    {
        var last = _hearts.Last();
        Destroy(last);
        _hearts.Remove(last);
    }
    private void OnHealthChanged(object sender, int e)
    {
        while (e > _hearts.Count)
            RemoveHeart();
        while (e < _hearts.Count)
            AddHeart();
    }
}
