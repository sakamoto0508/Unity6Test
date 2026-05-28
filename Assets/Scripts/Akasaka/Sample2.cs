using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sample2 : MonoBehaviour
{
    // 行数
    [SerializeField] private int _rows = 5;
    // 列数
    [SerializeField] private int _columns = 5;
    private ImageObj[][] _imageObj;
    private GridLayoutGroup _layoutGroup;
    private int _currentIndexX = 0;
    private int _currentIndexY = 0;
    private void Start()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layoutGroup.constraintCount = _columns;
        _imageObj = new ImageObj[_rows][];
        for (var r = 0; r < _rows; r++)
        {
            _imageObj[r] = new ImageObj[_columns];
            for (var c = 0; c < _columns; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                var imageObj = obj.AddComponent<ImageObj>();
                imageObj.Initialize(image);
                _imageObj[r][c] = imageObj;
                Debug.Log($"Cell({r}, {c}) initialized.");
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }
            }
        }
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) { return; } // 入力デバイスがない場合は処理しない

        if (keyboard.leftArrowKey.wasPressedThisFrame) // 左キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            for (int i = 1; i <= _columns; i++)
            {
                int nextX = (_currentIndexX - i + _columns) % _columns;

                if (_imageObj[_currentIndexY][nextX].IsVisible)
                {
                    _currentIndexX = nextX;
                    break;
                }
            }
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
        if (keyboard.rightArrowKey.wasPressedThisFrame) // 右キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            for (int i = 1; i <= _columns; i++)
            {
                int nextX = (_currentIndexX + i) % _columns;

                if (_imageObj[_currentIndexY][nextX].IsVisible)
                {
                    _currentIndexX = nextX;
                    break;
                }
            }
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
        if (keyboard.upArrowKey.wasPressedThisFrame) // 上キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            for (int i = 1; i <= _rows; i++)
            {
                int nextY = (_currentIndexY - i + _rows) % _rows;
                if(_imageObj[nextY][_currentIndexX].IsVisible)
                {
                    _currentIndexY = nextY;
                    break;
                }
            }
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
        if (keyboard.downArrowKey.wasPressedThisFrame) // 下キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            for (int i = 1; i <= _rows; i++)
            {
                int nextY = (_currentIndexY + i) % _rows;
                if(_imageObj[nextY][_currentIndexX].IsVisible)
                {
                    _currentIndexY = nextY;
                    break;
                }
            }
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }

        if (keyboard.spaceKey.wasPressedThisFrame) // スペースキーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsVisible(false);
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            //一番近くのセルをアクティブにする
            var nearestIndex = SerachNeaestIndex(_currentIndexX, _currentIndexY);
            _currentIndexX = nearestIndex[0];
            _currentIndexY = nearestIndex[1];
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
    }

    private int[] SerachNeaestIndex(int currentX, int currentY)
    {
        var nearestIndexX = currentX;
        var nearestIndexY = currentY;
        var minDistance = int.MaxValue;
        for (int j = 0; j < _columns; j++)
        {
            for (int i = 0; i < _rows; i++)
            {
                var distance = Mathf.Abs(currentX - j) + Mathf.Abs(currentY - i);
                if (distance < minDistance && _imageObj[i][j].IsVisible)
                {
                    nearestIndexX = j;
                    nearestIndexY = i;
                    minDistance = distance;
                }
            }
        }
        return new int[] { nearestIndexX, nearestIndexY };
    }
}
