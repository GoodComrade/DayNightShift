using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameFlowCanvas;

    [SerializeField]
    private List<ActivityCanvasRevealer> _activityRevealers;

    private List<ActivityCanvas> _activityCanvasList = new List<ActivityCanvas>();
    private GameObject _lastRevealedCanvas;

    private void Awake()
    {
        _activityRevealers.ForEach(canvas => _activityCanvasList.Add(canvas.ActivityToShow));
        _activityCanvasList.ForEach(activity => activity.SetManager(this));
    }

    public void OnActionBegan(string canvasName)
    {
        _activityRevealers.ForEach(canvas =>
        {
            if (canvas.gameObject.name == canvasName)
            {
                _lastRevealedCanvas = canvas.ActivityToShow.gameObject;
                ShowTargetCanvas(_gameFlowCanvas, _lastRevealedCanvas);
            }
        });
    }

    public void ReturnToGameFlowCanvas() => ShowTargetCanvas(_lastRevealedCanvas, _gameFlowCanvas);

    private void ShowTargetCanvas(GameObject old, GameObject target)
    {
        old.SetActive(false);
        target.SetActive(true);
    }
}
