using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target { get; private set; }

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        Target = _target;
    }
    void LateUpdate()
    {
        Vector3 desirePosition = _target.position + offset;
        Vector3 smoothPosition = Vector3.LerpUnclamped(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
