using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Viable))]
[RequireComponent(typeof(Renderer))]
public class InvincibleViability : Modification<Viable>
{
    public bool IsInvincible { get; set; }
    [SerializeField] private float _blinkingFrequency;
    [SerializeField] private List<Renderer> _renderers;
    private List<Color> _defaultColors;
    private List<Color> _transparentColors;
    public override void Modify(Viable sender)
    {
        sender.Health++;
    }

    private void Start()
    {
        _renderers = GetComponents<Renderer>().ToList();
        _defaultColors = _renderers.Select(x => x.material.color).ToList();
        _transparentColors = _defaultColors.Select(x => { x.a = 0f; return x; }).ToList();
    }
    private void Update()
    {
        if (IsInvincible)
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
