using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セルの状態を表す列挙型
/// </summary>
public enum CellState
{
    Dead,
    Alive
}

public class CellScript : MonoBehaviour
{
    public CellState State
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnStateChanged();
        }
    }

    //veiw
    [SerializeField] private Image _image;
    //生きているときの色
    [SerializeField] private Color _aliveColor = Color.green;
    //死んでいるときの色
    [SerializeField] private Color _deadColor = Color.black;
    //セルの状態
    [SerializeField] private CellState _cellState;

    private void Start()
    {
        //初期状態を反映
        OnStateChanged();
    }

    //セルの状態を変更するメソッド
    private void OnStateChanged()
    {
        switch (_cellState)
        {
            case CellState.Dead:
                _image.color = _deadColor;
                break;
            case CellState.Alive:
                _image.color = _aliveColor;
                break;
        }
    }


    private void OnValidate()
    {
        //エディタ上で状態が変更されたときに色を更新
        _image.color = (State == CellState.Alive) ? _aliveColor : _deadColor;
    }
}
