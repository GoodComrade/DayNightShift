using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class PropTransparenter : MonoBehaviour
{
    public Material Material { get; private set; }

    private static readonly int Transparency = Shader.PropertyToID("_Color");

    private const float MaxTransparencyValue = 1f;

    private float _transparentingDuration;
    private float _minTransparencyValue;

    private MeshRenderer _meshRenderer;

    private Tween RevealTween;
    private Tween UnRevealTween;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        Material = _meshRenderer.material;

    }

    public void SetParams(float minTransparencyValue, float transparentingDuration)
    {
        _minTransparencyValue = minTransparencyValue;
        _transparentingDuration = transparentingDuration;
    }

    public void Reveal()
    {
        if (Material.color.a >= 1f)
            return;

        if (UnRevealTween != null)
            UnRevealTween.Kill();

        RevealTween = Material.DOFade(MaxTransparencyValue, Transparency, _transparentingDuration);
    }

    public void UnReveal()
    {
        if (Material.color.a <= _minTransparencyValue)
            return;

        if (RevealTween != null)
            RevealTween.Pause();

        UnRevealTween = Material.DOFade(_minTransparencyValue, Transparency, _transparentingDuration);
    }

    public void RevealWithRendererEnable()
    {
        if (Material.color.a >= 1f)
            return;

        if (UnRevealTween != null)
            UnRevealTween.Kill();

        EnableRenderer();
        RevealTween = Material.DOFade(MaxTransparencyValue, Transparency, _transparentingDuration);
    }

    public void UnRevealWithRendererDisable()
    {
        if (Material.color.a <= _minTransparencyValue)
            return;

        if (RevealTween != null)
            RevealTween.Pause();

        UnRevealTween = Material.DOFade(_minTransparencyValue, Transparency, _transparentingDuration).OnComplete(DisaberlRenderer);
    }

    public void DisaberlRenderer() => _meshRenderer.enabled = false;

    public void EnableRenderer() => _meshRenderer.enabled = true;

}
