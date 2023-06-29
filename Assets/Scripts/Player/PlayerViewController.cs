using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class PlayerViewController : MonoBehaviour
{
    private const string REVEALER_TAG = "CanvasRevealer";

    public event UnityAction<string> OnActionBegan;

    [SerializeField]
    private GameObject _playerActionCanvas;

    [SerializeField]
    private float _moveSpeed;

    private PlayerInput _playerInput;

    private Vector2 _direction;
    private Vector2 _rotation;

    private bool _isAction = false;
    private string _lastAcrionRevealerName;

    private void Awake()
    {
        _playerActionCanvas.SetActive(false);
        _playerInput = new PlayerInput();

        _playerInput.Player.Action.performed += context => OnActionBegin();
        //_playerInput.Player.Move.performed += context => OnMove();
    }

    private void Update()
    {
        OnMove();
        Move(_direction);
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnMove()
    {
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
    }

    private void Move(Vector2 direction)
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        transform.Translate(movement * _moveSpeed * Time.deltaTime, Space.World);

        //Когда будут готовы спрайты, реализовать через них поворот игрока
        /*if (movement != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movement, Vector3.up);*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == REVEALER_TAG)
        {
            SetActionCondition(true);
            _lastAcrionRevealerName = collision.gameObject.name;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == REVEALER_TAG)
            SetActionCondition(false);
    }

    private void SetActionCondition(bool condition)
    {
        _isAction = condition;
        _playerActionCanvas.SetActive(condition);
    }

    private void OnActionBegin()
    {
        if (_isAction == false)
            return;

        OnActionBegan?.Invoke(_lastAcrionRevealerName);
    }
}
