using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerViewMover : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField]
    private float _moveSpeed;

    private Vector2 _direction;
    private Vector2 _rotation;

    private void Awake()
    {
        _playerInput = new PlayerInput();

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
}
