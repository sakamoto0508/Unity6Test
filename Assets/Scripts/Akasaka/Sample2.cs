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
            _currentIndexX = (_currentIndexX - 1 + _columns) % _columns;
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
        if (keyboard.rightArrowKey.wasPressedThisFrame) // 右キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            _currentIndexX = (_currentIndexX + 1) % _columns;
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }
        if (keyboard.upArrowKey.wasPressedThisFrame) // 上キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            _currentIndexY = (_currentIndexY - 1 + _rows) % _rows;
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }

        if (keyboard.downArrowKey.wasPressedThisFrame) // 下キーを押した
        {
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(false);
            _currentIndexY = (_currentIndexY + 1) % _rows;
            _imageObj[_currentIndexY][_currentIndexX].SetIsActive(true);
        }

        if (keyboard.spaceKey.wasPressedThisFrame) // スペースキーを押した
        {
           
            
        }
    }
}
