using UnityEngine;
using UnityEngine.UI;

public class ImageObj2 : MonoBehaviour
{
    public bool IsBlack { get; private set; }
    public int Row{ get; private set; }
    public int Columns{ get; private set; }

    private Image _image;
    public void Initialize(Image image, int row, int columns)
    {
        _image = image;
        Row = row;
        Columns = columns;
    }

    public void SetIsBlack()
    {
        IsBlack = true;
        _image.color = Color.black;
    }

    public void SetIsWhite()
    {
        IsBlack = false;
        _image.color = Color.white;
    }
}
