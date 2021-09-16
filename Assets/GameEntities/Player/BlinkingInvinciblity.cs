using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BlinkingInvinciblity : MonoBehaviour
{
    [SerializeField] private float _blinkingFrequency;
    private Health _health;
    private List<Renderer> _renderers;
    private List<Color> _defaultColors;
    private List<Color> _transparentColors;
    private void Start()
    {
        _health = GetComponent<Health>();
        _renderers = GetComponents<Renderer>().ToList();
        _defaultColors = _renderers.Select(x => x.material.color).ToList();
        _transparentColors = _defaultColors.Select(x => { x.a = 0f; return x; }).ToList();
    }
    private void Update()
    {
        if (_health.IsInvincible)
        {
            if (Time.time % _blinkingFrequency > _blinkingFrequency / 2)
                SetColors(_defaultColors);
            else
                SetColors(_transparentColors);
        }
    }
    private void SetColors(List<Color> color)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material.color = color[i];
        }
    }
}
