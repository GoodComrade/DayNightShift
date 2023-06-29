using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityCanvasRevealer : MonoBehaviour
{
    public ActivityCanvas ActivityToShow => _activityToShow;

    [SerializeField]
    private ActivityCanvas _activityToShow;
}
