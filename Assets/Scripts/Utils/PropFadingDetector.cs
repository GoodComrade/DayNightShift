using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CameraFollow))]
public class PropFadingDetector : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _propsObjects;

    private List<PropTransparenter> _propsTransparenters = new List<PropTransparenter>();

    private Transform _target;
    private RaycastHit _hit;

    private void Awake()
    {
        
    }
    void Start()
    {
        _target = GetComponent<CameraFollow>().Target;
        foreach (var prop in _propsObjects)
            _propsTransparenters.Add(prop.GetComponent<PropTransparenter>());
    }

    //попробуй переделать это на события, где если имя объекта не совпадает, то и прозрачность не выставляется

    void Update()
    {
        Vector3 rayDirection = _target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out _hit))
        {
            Debug.DrawRay(_target.position, transform.position, Color.red);

            if (_hit.collider.gameObject.name != _target.gameObject.name)
            {
                var detectedProp = _propsTransparenters.First(prop => prop.gameObject.name == _hit.collider.gameObject.name);
                detectedProp.SetDetectedProp(_hit.collider.gameObject.name);
            }
            else
            {
                _propsTransparenters.ForEach(prop => prop.SetDetectedProp(_target.gameObject.name));
            }
            //OnPropBetween.Invoke(_hit.collider.gameObject.name);
        }
    }
}
