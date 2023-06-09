using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CameraFollow))]
public class PropFadingDetector : MonoBehaviour
{
    public event UnityAction<string> OnPropDetection;

    [SerializeField]
    private float _transparentingDuration;
    [SerializeField]
    private float _minTransparencyValue;
    [SerializeField]
    private List<PropTransparenter> _propsTransparenters = new List<PropTransparenter>();

    private Transform _target;
    private RaycastHit _hit;
    private string _lastDetectedProp;

    private void Awake()
    {
        foreach(var prop in _propsTransparenters)
        {
            OnPropDetection += prop.SetDetectedProp;
            prop.SetParams(_minTransparencyValue, _transparentingDuration);
        }
    }

    void Start()
    {
        _target = GetComponent<CameraFollow>().Target;
    }

    //попробуй переделать это на события, где если имя объекта не совпадает, то и прозрачность не выставляется

    void Update()
    {
        Vector3 rayDirection = _target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out _hit))
        {
            if(_hit.collider.gameObject.name != _target.gameObject.name)
            {
                if(_hit.collider.gameObject.name != _lastDetectedProp)
                {
                    OnPropDetection.Invoke(_hit.collider.gameObject.name);
                    _lastDetectedProp = _hit.collider.gameObject.name;
                }
            }
            
            if(_hit.collider.gameObject.name == _target.gameObject.name && _hit.collider.gameObject.name != _lastDetectedProp)
            {
                OnPropDetection.Invoke(_hit.collider.gameObject.name);
                _lastDetectedProp = _target.gameObject.name;
            }
        }
    }
}
