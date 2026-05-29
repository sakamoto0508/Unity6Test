using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TicTacToe : MonoBehaviour
{
    private const int Size = 3;

    private Image[,] _cells;

    [SerializeField]
    private Color _normalCell = Color.white;

    [SerializeField]
    private Color _selectedCell = Color.cyan;

    private int _selectedRow;
    private int _selectedColumn;

    [SerializeField]
    private Sprite _circle = null;

    [SerializeField]
    private Sprite _cross = null;
    // ○のターンかどうか
    private bool _isCircleTurn = true;
    //勝利しているか
    private bool _isWin = false;
    private bool _isFull = false;


    private void Start()
    {
        _cells = new Image[Size, Size];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r},{c})");
                obj.transform.parent = transform;
                var cell = obj.AddComponent<Image>();
                _cells[r, c] = cell;
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) { _selectedColumn--; }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) { _selectedColumn++; }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) { _selectedRow--; }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) { _selectedRow++; }

        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= Size) { _selectedColumn = Size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= Size) { _selectedRow = Size - 1; }

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = _cells[r, c];
                cell.color =
                    (r == _selectedRow && c == _selectedColumn)
                    ? _selectedCell : _normalCell;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var cell = _cells[_selectedRow, _selectedColumn];

            // 既にマークがない場合のみ、マークをつける。
            if (cell.sprite == null && !_isWin && _isCircleTurn)
            {
                cell.sprite = _isCircleTurn ? _circle : _cross;
                _isCircleTurn = !_isCircleTurn;
                //敵のターンを作る。
                EnemyTurn();
            }

            //勝利判定
            for (int i = 0; i < Size; i++)
            {
                // 横方向の勝利判定
                if (_cells[i, 0].sprite != null &&
                    _cells[i, 0].sprite == _cells[i, 1].sprite &&
                    _cells[i, 1].sprite == _cells[i, 2].sprite)
                {
                    _isWin = true;
                    break;
                }

                // 縦方向の勝利判定
                if (_cells[0, i].sprite != null &&
                    _cells[0, i].sprite == _cells[1, i].sprite &&
                    _cells[1, i].sprite == _cells[2, i].sprite)
                {
                    _isWin = true;
                    break;
                }
            }

            // 斜め方向の勝利判定
            if (_cells[0, 0].sprite != null &&
                _cells[0, 0].sprite == _cells[1, 1].sprite &&
                _cells[1, 1].sprite == _cells[2, 2].sprite)
            {
                _isWin = true;
            }

            if (_cells[0, 2].sprite != null &&
                _cells[0, 2].sprite == _cells[1, 1].sprite &&
                _cells[1, 1].sprite == _cells[2, 0].sprite)
            {
                _isWin = true;
            }
            if (_isWin)
            {
                Debug.Log($"Win: {(_isCircleTurn ? "Cross" : "Circle")}");
                StartCoroutine(Rest());
            }
        }
    }

    private void EnemyTurn()
    {
        int randX = Random.Range(0, Size);
        int randY = Random.Range(0, Size);
        var cell = _cells[randX, randY];
        if (cell.sprite == null)
        {
            cell.sprite = _cross;
            _isCircleTurn = !_isCircleTurn;
        }
        else if (!_isWin)
        {
            EnemyTurn();
        }
    }


    private IEnumerator Rest()
    {
        yield return new WaitForSeconds(3f);
        _isWin = false;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                _cells[i, j].sprite = null;
            }
        }
        _isCircleTurn = true;
    }
}