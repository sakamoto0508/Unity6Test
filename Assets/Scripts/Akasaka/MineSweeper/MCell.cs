using UnityEngine;
using UnityEngine.UI;

public class MCell : MonoBehaviour
{
    [SerializeField] private Text _view = null;

    [SerializeField] private MCellState _cellState = MCellState.None;
    [SerializeField] private Color _hiddenColor= Color.aliceBlue;

    public MCellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    public bool IsHidden { get; private set; }
    private Image _hiddenImage;

    public void IsOpen()
    {
        IsHidden = false;
        _hiddenImage.color = Color.white;
        OnCellStateChanged();
    }

    private void Start()
    {
        _hiddenImage = GetComponent<Image>();
        IsHidden = true;
        _hiddenImage.color = _hiddenColor;
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (_view == null) { return; }

        if (_cellState == MCellState.None)
        {
            _view.text = "";
        }
        else if (IsHidden)
        {
            _view.text = "";
        }
        else if (_cellState == MCellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }
}