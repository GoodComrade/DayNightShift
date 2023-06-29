using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCanvas : MonoBehaviour
{
    [SerializeField]
    private Button _returnButton;

    private PlayerCanvasManager _manager;

    private void OnEnable()
    {
        _returnButton.onClick.AddListener(OnEndAction);
    }

    private void OnDisable()
    {
        _returnButton?.onClick.RemoveListener(OnEndAction);
    }

    public void SetManager(PlayerCanvasManager manager) => _manager = manager;

    private void OnEndAction() => _manager.ReturnToGameFlowCanvas();
}
