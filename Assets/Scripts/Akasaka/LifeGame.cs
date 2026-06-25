using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private CellScript _cellPrefab;
    [SerializeField] private int _rows = 10;
    [SerializeField] private int _columns = 10;
    [SerializeField,Multiline] private string _data;
    [SerializeField] private float _duration = 1.0F; // セルを更新する時間間隔（秒単位）
    private bool _isPlaying = false; // 時間経過の更新が実行中かどうか
    private float _currentTime = 0.0F; // 経過時間を追跡する変数

    private CellScript[,] _cells;

    private void Start()
    {
        _cells = new CellScript[_rows, _columns];
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;
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
        StartPattern(_data);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) { return; }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            _isPlaying = !_isPlaying;
        }

        if (_isPlaying)
        {
            _currentTime += Time.deltaTime;

            while (_currentTime >= _duration)
            {
                _currentTime -= _duration;
                OnNext();
            }
        }
        else
        {
            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                OnNext();
            }
        }
    }

    private void OnNext()
    {
        CellCheck();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var target = eventData.pointerCurrentRaycast.gameObject;
        if (target.TryGetComponent<CellScript>(out var cell))
        {
            cell.State = cell.State == CellState.Alive ? CellState.Dead : CellState.Alive;
        }
    }

    private void StartPattern(string pattern)
    {
        if(pattern.Length < _rows * _columns) 
        {
            while(pattern.Length < _rows * _columns)
            {
                pattern += "0";
            }   
        }
        for(int i=0;i<_rows;i++)
        {
            for(int j=0;j<_columns;j++)
            {
                _cells[i, j].State = pattern[i * _columns + j] == '1' ? CellState.Alive : CellState.Dead;
            }
        }
    }

    private int CountAliveNeighbors(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // 自分自身はカウントしない
                int neighborX = x + i;
                int neighborY = y + j;
                if (neighborX >= 0 && neighborX < _columns && neighborY >= 0 && neighborY < _rows)
                {
                    if (_cells[neighborY, neighborX].State == CellState.Alive)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //セルが次の世代で生きるか死ぬかを判定する
    private void CellCheck()
    {
        CellState[,] nextStates = new CellState[_rows, _columns];
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                int aliveNeighbors = CountAliveNeighbors(c, r);
                if (_cells[r, c].State == CellState.Alive)
                {
                    // 生きているセルのルール
                    if (aliveNeighbors < 2 || aliveNeighbors > 3)
                    {
                        nextStates[r, c] = CellState.Dead; // 過疎または過密で死ぬ
                    }
                    else
                    {
                        nextStates[r, c] = CellState.Alive; // 生き続ける
                    }
                }
                else
                {
                    // 死んでいるセルのルール
                    if (aliveNeighbors == 3)
                    {
                        nextStates[r, c] = CellState.Alive; // 誕生する
                    }
                    else
                    {
                        nextStates[r, c] = CellState.Dead; // 死んだまま
                    }
                }
            }
        }
        // 次の世代の状態を反映する
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                _cells[r, c].State = nextStates[r, c];
            }
        }
    }
}
