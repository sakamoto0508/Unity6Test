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
    private int _currentIndexX = 0;
    private int _currentIndexY = 0;
    private ImageObj2[][] _imageObj;
    private void Start()
    {
        _imageObj = new ImageObj2[5][];
        for (var r = 0; r < 5; r++)
        {
            _imageObj[r] = new ImageObj2[5];
            for (var c = 0; c < 5; c++)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var imageObj = cell.GetComponent<ImageObj2>();
        if (imageObj.IsBlack)
        {
            imageObj.SetIsWhite();
        }
        else
        {
            imageObj.SetIsBlack();
        }
    }
}
