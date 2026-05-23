using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] private int _maxCells = 5;
    private ImageObj[] _cellPrefabs;
    private int _currentCellIndex;

    private void Start()
    {
        _cellPrefabs = new ImageObj[_maxCells];

        _currentCellIndex = 0;

        for (var i = 0; i < _maxCells; i++)
        {
            var obj = new GameObject($"Cell{i}");

            var image = obj.AddComponent<Image>();

            var imageObj = obj.AddComponent<ImageObj>();

            imageObj.Initialize(image);

            obj.transform.SetParent(transform, false);

            _cellPrefabs[i] = imageObj;

            if (i == 0)
                imageObj.SetIsActive(true);
            else
                imageObj.SetIsActive(false);
        }
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) { return; } // 入力デバイスがない場合は処理しない

        if (keyboard.leftArrowKey.wasPressedThisFrame) // 左キーを押した
        {
            if (_currentCellIndex > 0)
            {
                // 左探索
                for (int i = _currentCellIndex - 1; i >= 0; i--)
                {
                    // 透明でないセルを見つけるまで左に探索
                    if (_cellPrefabs[i].IsVisible)
                    {
                        // 現在のセルを白にする
                        _cellPrefabs[_currentCellIndex].SetIsActive(false);

                        _currentCellIndex = i;
                        // 新しいセルを赤にする
                        _cellPrefabs[_currentCellIndex].SetIsActive(true);

                        break;
                    }
                }
            }
        }
        if (keyboard.rightArrowKey.wasPressedThisFrame) // 右キーを押した
        {
            if (_currentCellIndex < _cellPrefabs.Length - 1)
            {
                // 右探索
                for (int i = _currentCellIndex + 1; i < _cellPrefabs.Length; i++)
                {
                    // 透明でないセルを見つけるまで右に探索
                    if (_cellPrefabs[i].IsVisible)
                    {
                        // 現在のセルを白にする
                        _cellPrefabs[_currentCellIndex].SetIsActive(false);

                        _currentCellIndex = i;
                        // 新しいセルを赤にする
                        _cellPrefabs[_currentCellIndex].SetIsActive(true);

                        break;
                    }
                }

            }
        }

        if (keyboard.spaceKey.wasPressedThisFrame) // スペースキーを押した
        {
            // 現在のセルを透明にする
            _cellPrefabs[_currentCellIndex].SetIsVisible(!_cellPrefabs[_currentCellIndex].IsVisible);
            // 現在のセルを非選択化
            _cellPrefabs[_currentCellIndex].SetIsActive(false); 

            int nextIndex = -1;

            // 右探索
            for (int i = _currentCellIndex + 1; i < _cellPrefabs.Length; i++)
            {
                if (_cellPrefabs[i].IsVisible)
                {
                    nextIndex = i;
                    break;
                }
            }

            // 左探索
            if (nextIndex == -1)
            {
                for (int i = _currentCellIndex - 1; i >= 0; i--)
                {
                    if (_cellPrefabs[i].IsVisible)
                    {
                        nextIndex = i;
                        break;
                    }
                }
            }

            // 次セル選択
            if (nextIndex != -1)
            {
                _currentCellIndex = nextIndex;
                _cellPrefabs[_currentCellIndex].SetIsActive(true);
            }
        }
    }
}