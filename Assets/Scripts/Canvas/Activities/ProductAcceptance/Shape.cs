using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : Product
{
    [SerializeField]
    private List<Image> _images;
    public bool canRotate = true;

    public Vector3 queueOffset;

    private void Move(Vector3 moveDirection) => transform.position += moveDirection;

    public void MoveLeft() => Move(new Vector3(-1, 0, 0));

    public void MoveRight() => Move(new Vector3(1, 0, 0));

    public void MoveUp() => Move(new Vector3(0, 1, 0));

    public void MoveDown() => Move(new Vector3(0, -1, 0));

    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }
    
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void RotateClockwise(bool rotateClockwise)
    {
        if (rotateClockwise)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
    }

    public override void TurnImageReycastTarget(bool value)
    {
        _images.ForEach(image => image.raycastTarget = value);
    }
}
