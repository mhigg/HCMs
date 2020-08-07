using UnityEngine;
using UnityEngine.UI;

public class ImageNo : SpriteNo<Image>
{
    [SerializeField]
    private bool _raycastTarget = false;

    // 新しく作ったImageの初期化
    protected override void InitComponent(Image image)
    {
        image.raycastTarget = _raycastTarget;
    }

    // Spriteを更新
    protected override void UpdateComponent(Image image, Sprite sprite, Color color)
    {
        image.sprite = sprite;
        image.color = color;
        image.SetNativeSize();
    }

    // RaycastTargetの設定を変更する
    public void ChangeRaycastTarget(bool raycastTarget)
    {
        _raycastTarget = raycastTarget;
        InitComponents();
    }

}