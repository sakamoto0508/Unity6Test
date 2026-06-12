using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sample3 : MonoBehaviour, IPointerClickHandler
{
    // 行数
    [SerializeField] private int _rows = 5;
    // 列数
    [SerializeField] private int _columns = 5;
    private GridLayoutGroup _layoutGroup;
    private ImageObj2[][] _imageObj;
    private bool _isClear;
    private int _count = 0;
    private float _timer = 0f;
    private void Start()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layoutGroup.constraintCount = _columns;
        _imageObj = new ImageObj2[_rows][];

        for (var r = 0; r < _rows; r++)
        {
            _imageObj[r] = new ImageObj2[_columns];
            for (var c = 0; c < _columns; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();
                var imageObj = cell.AddComponent<ImageObj2>();
                imageObj.Initialize(image);
                _imageObj[r][c] = imageObj;
            }
        }
    }

    private void Update()
    {
        if (!_isClear)
        {
            _timer += Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var imageObj = cell.GetComponent<ImageObj2>();

        if (!_isClear)
            _count++;

        if (imageObj.IsBlack)
        {
            imageObj.SetIsWhite();
        }
        else
        {
            imageObj.SetIsBlack();
        }
        if (!_isClear)
        {
            _isClear = ClearCheck();
            if (_isClear)
            {
                Debug.Log($"Clear! Time: {_timer} Count: {_count}");
            }
        }
    }

    //すべてのセルが白または黒であるかをチェックする
    private bool ClearCheck()
    {
        bool isAllCollected = true;
        bool isBlack = true;
        for (int r = 0; r < _rows; r++)
        {
            if (_imageObj[0][0].IsBlack)
            {
                isBlack = true;
            }
            for (int c = 0; c < _columns; c++)
            {
                if (_imageObj[r][c].IsBlack != isBlack && !_imageObj[r][c].IsBlack != !isBlack)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
