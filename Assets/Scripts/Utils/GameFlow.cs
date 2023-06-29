using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField]
    private PlayerViewController _playerViewController;

    [SerializeField]
    private PlayerCanvasManager _playerCanvasManager;

    private void OnEnable()
    {
        _playerViewController.OnActionBegan += _playerCanvasManager.OnActionBegan;
    }
}
