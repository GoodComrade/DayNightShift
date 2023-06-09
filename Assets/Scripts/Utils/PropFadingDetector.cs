using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PropFadingDetector : MonoBehaviour
{
    [SerializeField]
    private float _transparentingDuration;
    [SerializeField]
    private float _minTransparencyValue;
    [SerializeField]
    private List<PropTransparenter> _propsTransparenters = new List<PropTransparenter>();

    private RaycastHit _hit;
    private string _lastDetectedProp;

    private void Awake()
    {
        DOTween.SetTweensCapacity(3000, 200);
        foreach(var prop in _propsTransparenters)
        {
            prop.SetParams(_minTransparencyValue, _transparentingDuration);
        }
    }

    void Start()
    {

    }

    //попробуй переделать это на события, где если имя объекта не совпадает, то и прозрачность не выставляется

    void Update()
    {

        Debug.Log(_lastDetectedProp);
        if (Physics.Raycast(transform.position, -Vector3.forward, out _hit, 1f))
        {
            Debug.DrawRay(transform.position, -transform.forward, Color.red);

            if (_hit.collider.gameObject.name != _lastDetectedProp)
            {
                if (string.IsNullOrEmpty(_lastDetectedProp) || _lastDetectedProp == gameObject.name)
                {
                    _propsTransparenters.First(prop => prop.gameObject.name == _hit.collider.gameObject.name).UnReveal();
                    _lastDetectedProp = _hit.collider.gameObject.name;
                    return;
                }


                _propsTransparenters.First(prop => prop.gameObject.name == _lastDetectedProp).Reveal();
                _propsTransparenters.First(prop => prop.gameObject.name == _hit.collider.gameObject.name).UnReveal();
                _lastDetectedProp = _hit.collider.gameObject.name;
                //OnPropDetection.Invoke(_hit.collider.gameObject.name);

            }

            /*if (_hit.collider.gameObject.name == _target.gameObject.name && _hit.collider.gameObject.name != _lastDetectedProp)
            {
                OnPropDetection.Invoke(_hit.collider.gameObject.name);
                _lastDetectedProp = _target.gameObject.name;
            }*/
        }
        else
        {
            if (string.IsNullOrEmpty(_lastDetectedProp) || _lastDetectedProp == gameObject.name)
                return;

            _propsTransparenters.First(prop => prop.gameObject.name == _lastDetectedProp).Reveal();
            _lastDetectedProp = null;
        }
    }

}
