using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField] private MCell _cellPrefab = null;

    [SerializeField] private int _rows = 10;
    [SerializeField] private int _columns = 10;
    [SerializeField] private int _mineCount = 10;

    private MCell[,] _cells;

    private bool _gameOver = false;
    private bool _cleared = false;

    private void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;
        _cells = new MCell[_rows, _columns];
        var parent = _gridLayoutGroup.gameObject.transform;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab, parent);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
        for (var i = 0; i < _mineCount; i++)
        {
            ChangeMine(_cells);
        }
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                if (_cells[r, c].CellState != MCellState.Mine)
                {
                    int count = CountMine(_cells, r, c);
                    ChangeCell(_cells[r, c], count);
                }
            }
        }
        StartCell();

        _gameOver = false;
        _cleared = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject.GetComponent<MCell>();
        if (cell != null && cell.IsHidden && !_gameOver)
        {
            cell.IsOpen();
            if (cell.CellState == MCellState.Mine)
            {
                _gameOver = true;
                Debug.Log("Game Over!");
            }

            CheckCleared();
        }
    }

    private void StartCell()
    {
        int randomRow = Random.Range(0, _rows);
        int randomColumn = Random.Range(0, _columns);
        var cell = _cells[randomRow, randomColumn];
        if (cell.CellState == MCellState.Mine)
        {
            StartCell();
            return;
        }
        cell.IsOpen();
    }


    // 地雷をランダムに配置するメソッド
    private void ChangeMine(MCell[,] cells)
    {
        var r = Random.Range(0, _rows);
        var c = Random.Range(0, _columns);
        var cell = cells[r, c];
        if (cell.CellState == MCellState.Mine)
        {
            ChangeMine(cells);
            return;
        }
        cell.CellState = MCellState.Mine;
    }

    // 周囲の地雷の数を数えるメソッド
    private int CountMine(MCell[,] cells, int row, int column)
    {
        int count = 0;

        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = column - 1; c <= column + 1; c++)
            {
                if (r >= 0 && r < _rows && c >= 0 && c < _columns)
                {
                    if (cells[r, c].CellState == MCellState.Mine)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    // セルを変更するメソッド
    private void ChangeCell(MCell cell, int count)
    {
        cell.CellState = (MCellState)count;
    }

    private bool CheckCleared()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = _cells[r, c];
                if (cell.CellState != MCellState.Mine && cell.IsHidden)
                {
                    return false;
                }
            }
        }
        Debug.Log("Cleared!");
        return true;
    }
}