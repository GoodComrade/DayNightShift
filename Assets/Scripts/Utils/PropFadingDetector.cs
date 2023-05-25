using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CameraFollow))]
public class PropFadingDetector : MonoBehaviour
{
    public event UnityAction<string> OnPropBetween;

    [SerializeField]
    private List<PropTransparenter> _props;

    private Transform _target;
    private RaycastHit _hit;

    void Start()
    {
        _target = GetComponent<CameraFollow>().Target;
        foreach (var prop in _props)
        {
            OnPropBetween += prop.SetDetectedProp;
        }
    }

    //попробуй переделать это на события, где если имя объекта не совпадает, то и прозрачность не выставляется

    void Update()
    {
        Vector3 rayDirection = _target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out _hit))
        {
            OnPropBetween.Invoke(_hit.collider.gameObject.name);
        }
    }
}
