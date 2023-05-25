using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class PropTransparenter : MonoBehaviour
{
    private const float MaxTransparencyValue = 1f;

    private static readonly int Transparency = Shader.PropertyToID("_Transparency");

    public bool IsPlayerBehind { get; private set; }
    public string DetectedProp { get; set; }

    [SerializeField]
    private float _transparentingSpeed;

    [SerializeField]
    private float _minTransparencyValue;

    private Material _material;
    private float _currentTransparency;


    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        IsPlayerBehind = false;
    }

    private void Update()
    {
        if (DetectedProp == gameObject.name)
        {
            var transparency = Mathf.Lerp(MaxTransparencyValue, _minTransparencyValue, _transparentingSpeed);
            
            SetTransparency(transparency);
        }
        else
        {
            var transparency = Mathf.Lerp(_minTransparencyValue, MaxTransparencyValue, _transparentingSpeed);
            SetTransparency(transparency);
        }
    }

    public void SetDetectedProp(string value) => DetectedProp = value;
    public void SetPlayerBehind(bool value) => IsPlayerBehind = value;

    private void Reveal()
    {
        var transparency = Mathf.Lerp(_minTransparencyValue, MaxTransparencyValue, _transparentingSpeed * Time.deltaTime);
        SetTransparency(transparency);
    }

    private void UnReveal()
    {
        var transparency = Mathf.Lerp(MaxTransparencyValue, _minTransparencyValue, _transparentingSpeed * Time.deltaTime);
        SetTransparency(transparency);
    }

    private void SetTransparency(float transparency)
    {
        if (Mathf.Abs(_currentTransparency - transparency) < 0.01f)
            return;

        _currentTransparency = transparency;
        SetHidePercentage(1f - _currentTransparency);
    }

    private void SetHidePercentage(float percentage) => _material.SetFloat(Transparency, Mathf.Abs(percentage - 1f));
}
