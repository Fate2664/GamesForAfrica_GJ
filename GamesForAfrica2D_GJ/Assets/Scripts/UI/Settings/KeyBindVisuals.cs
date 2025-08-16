using UnityEngine;
using Nova;
using System;

[System.Serializable]
public class KeyBindVisuals : ItemVisuals
{
    public TextBlock KeyFunctionText = null;
    public TextBlock KeyString = null;
    public UIBlock2D KeyBindBox = null;
    public UIBlock2D CancelBox = null;

    public Color DefaultColor;
    public Color HoverColor;
    public Color PressedColor;

    internal static void HandleHover(Gesture.OnHover evt, KeyBindVisuals target)
    {
        target.KeyBindBox.Color = target.HoverColor;
        AudioManager.Instance?.PlaySFX("HoverSound");
    }

    internal static void HandlePress(Gesture.OnPress evt, KeyBindVisuals target)
    {
        target.KeyBindBox.Color = target.PressedColor;
        AudioManager.Instance?.PlaySFX("ClickSound");
    }

    internal static void HandleRelease(Gesture.OnRelease evt, KeyBindVisuals target)
    {
        target.KeyBindBox.Color = target.HoverColor;
    }

    internal static void HandleUnhover(Gesture.OnUnhover evt, KeyBindVisuals target)
    {
        target.KeyBindBox.Color = target.DefaultColor;
    }
}
