using System.Collections;

using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private RectTransform _origin;
    [SerializeField] private RectTransform _emptySprite;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _header;
    [SerializeField] private int _completedRows = 0;

    private Transform[,] _grid;
    private Vector2 _offset;

    private void Awake()
    {
        RectTransform tileRect = _emptySprite.GetComponent<RectTransform>();
        tileRect.sizeDelta = _origin.sizeDelta;

        _offset = new Vector2(_origin.sizeDelta.x, _origin.sizeDelta.y);
    }

    private void OnEnable()
    {
        DrawGrid(_offset.x, _offset.y);
    }

    private void OnDisable()
    {
        
    }
    private void Start()
    {

    }


    private bool IsWithinBoard(int x, int y) => (x >= 0 && x < _width && y >= 0);

    private bool IsOccupied(int x, int y, Shape shape) =>
        _grid[x, y] != null && _grid[x, y].parent != shape.transform;

    private void DrawGrid(float xOffset, float yOffset)
    {
        _grid = new Transform[_width, _height];

        float startX = _origin.position.x;
        float startY = _origin.position.y;

        if (_emptySprite != null)
        {
            for (int y = 0; y < _height - _header; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var clone = Instantiate(_emptySprite, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), _emptySprite.transform.rotation);
                    clone.name =
                        "Board Space ( x = " + x.ToString() + " , y = " + y.ToString() + " )";
                    clone.transform.SetParent(transform);
                }
            }
        }
        else
        {
            Debug.Log("WARNING! Please assign the emptySprite object!");
        }
    }

    private void ClearGrid()
    {

    }

    private bool IsComplete(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void ClearRow(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_grid[x, y] != null)
            {
                Destroy(_grid[x, y].gameObject);
            }

            _grid[x, y] = null;
        }
    }

    private void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_grid[x, y] != null)
            {
                _grid[x, y - 1] = _grid[x, y];
                _grid[x, y] = null;
                _grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private void ShiftRowsDown(int startY)
    {
        for (int i = startY; i < _height; i++)
        {
            ShiftOneRowDown(i);
        }
    }

    /*private void ClearRowFx(int idx, int y)
    {
        if (rowGlowFx[idx])
        {
            rowGlowFx[idx].transform.position = new Vector3(0, y, -2);
            rowGlowFx[idx].Play();
        }
    }*/

    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);

            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (IsOccupied((int)pos.x, (int)pos.y, shape))
            {
                return false;
            }
        }

        return true;
    }

    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
        {
            return;
        }

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            _grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    /*public IEnumerator ClearAllRows()
    {
        completedRows = 0;

        for (int y = 0; y < height; y++)
        {
            if (IsComplete(y))
            {
                ClearRowFx(completedRows, y);
                completedRows++;
            }
        }

        yield return new WaitForSeconds(0.25f);

        for (int y = 0; y < height; y++)
        {
            if (IsComplete(y))
            {
                ClearRow(y);
                ShiftRowsDown(y + 1);
                yield return new WaitForSeconds(0.15f);
                y--;
            }
        }
    }*/

    public bool IsOverLimit(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= _height - _header - 1)
            {
                return true;
            }
        }

        return false;
    }
}
