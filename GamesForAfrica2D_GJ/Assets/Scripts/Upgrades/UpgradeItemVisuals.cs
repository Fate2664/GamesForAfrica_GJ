using Nova;
using System;
using UnityEngine;

[System.Serializable]
public class UpgradeItemVisuals : ItemVisuals
{
    public UIBlock contentRoot;
    public UIBlock2D Image;

    [Header("Animations")]
    public Color DefaultColor;
    public Color HoverColor;
    public Color PressedColor;

    public void Bind(UpgradeItem data)
    {
        if (data.isEmpty)
        {
            contentRoot.gameObject.SetActive(false);
            Debug.Log($"UpgradeItemVisuals: Bind called with empty data at index {data.count}");
        }
        else
        {
            contentRoot.gameObject.SetActive(true);
            Image.SetImage(data.item.Icon);
        }
    }

    internal static void HandleHover(Gesture.OnHover evt, UpgradeItemVisuals target, int index)
    {
        target.contentRoot.Color = target.HoverColor;
    }

    internal static void HandlePress(Gesture.OnPress evt, UpgradeItemVisuals target, int index)
    {
        target.contentRoot.Color = target.PressedColor;
        Debug.Log($"Pressed Upgrade");
    }

    internal static void HandleUnhover(Gesture.OnUnhover evt, UpgradeItemVisuals target, int index)
    {
        target.contentRoot.Color = target.DefaultColor;
    }

    internal static void HandleRelease(Gesture.OnRelease evt, UpgradeItemVisuals target, int index)
    {
        target.contentRoot.Color = target.HoverColor;
    }
}
