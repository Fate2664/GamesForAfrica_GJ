using Nova;
using UnityEngine;

[System.Serializable]
public class UpgradeItemVisuals : ItemVisuals
{
    public UIBlock contentRoot;
    public UIBlock2D Image;

    [Header("Animations")]
    public float duration = .15f;
    public BodyColorAnimation hoverAnimation;
    public BodyColorAnimation unhoverAnimation;
    public GradientColorAnimation pressAnimation;
    public GradientColorAnimation releaseAnimation;
    public UpgradeManager manager;
    public UpgradeData data;
    public PlayerStats.StatType stat;

    private AnimationHandle hoverHandle;
    private AnimationHandle pressHandle;

    public void Hover()
    {
        hoverHandle.Cancel();
        hoverHandle = hoverAnimation.Run(duration);
    }

    public void Unhover()
    {
        hoverHandle.Cancel();
        hoverHandle = unhoverAnimation.Run(duration);
    }

    public void Press()
    {
        pressHandle.Cancel();
        pressHandle = pressAnimation.Run(duration);
        manager.BuyUpgrade(data);
    }

    public void Release()
    {
        pressHandle.Cancel();
        pressHandle = releaseAnimation.Run(duration);
    }

    public void Bind(UpgradeItem data)
    {
        if (data.isEmpty)
        {
            contentRoot.gameObject.SetActive(false);
        }
        else
        {
            contentRoot.gameObject.SetActive(true);
            Image.SetImage(data.item.Icon);
        }
    }
}
