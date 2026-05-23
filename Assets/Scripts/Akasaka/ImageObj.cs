using UnityEngine;
using UnityEngine.UI;

public class ImageObj : MonoBehaviour
{
    /// <summary>
    /// アクティブなセルか
    /// </summary>
    public bool IsActive { get; private set; }
    /// <summary>
    /// 透明化かどうか
    /// </summary>
    public bool IsVisible { get; private set; } = true;

    private Image _image;

    public void Initialize(Image image)
    {
        _image = image;
    }

    public void SetIsVisible(bool isVisible)
    {
        IsVisible = isVisible;

        var color = _image.color;
        color.a = isVisible ? 1f : 0f;

        _image.color = color;
    }

    /// <summary>
    /// アクティブ状態かどうかのセット
    /// </summary>
    /// <param name="isActive"></param>
    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;

        var color = _image.color;

        color.r = 1f;
        color.g = isActive ? 0f : 1f;
        color.b = isActive ? 0f : 1f;

        _image.color = color;
    }
}