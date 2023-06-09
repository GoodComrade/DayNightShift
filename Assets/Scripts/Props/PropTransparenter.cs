using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class PropTransparenter : MonoBehaviour
{
    private const float MaxTransparencyValue = 1f;

    private static readonly int Transparency = Shader.PropertyToID("_Transparency");

    public string DetectedProp { get; set; }

    private float _transparentingDuration;
    private float _minTransparencyValue;

    private Material _material;


    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (DetectedProp == gameObject.name)
            Reveal();
        else
            UnReveal();
    }

    public void SetParams(float minTransparencyValue, float transparentingDuration)
    {
        _minTransparencyValue = minTransparencyValue;
        _transparentingDuration = transparentingDuration;
    }

    public void SetDetectedProp(string value) => DetectedProp = value;

    private void Reveal()
    {
        _material.DOFade(_minTransparencyValue, _transparentingDuration / 2);
    }

    private void UnReveal()
    {
        _material.DOFade(MaxTransparencyValue, _transparentingDuration);
    }


}
