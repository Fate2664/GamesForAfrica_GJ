using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class UpgradeDescription
{
    public string Name;
    public Texture2D Icon;

}

public class UpgradeItem
{
    public UpgradeDescription item;
    public int count;

    public bool isEmpty => this == Empty;

    public const int maxCount = 1;
    public static readonly UpgradeItem Empty = new UpgradeItem();
   
}

[CreateAssetMenu(menuName = "Upgrade/Items")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<UpgradeDescription> items = new List<UpgradeDescription>();

    public List<UpgradeItem> GetRandomItems(int count)
    {
        List<UpgradeItem> toRet = new List<UpgradeItem>();
        for (int i = 0; i < count ; i++)
        {
            if (Random.Range(0, 1f) > .5f)
            {
                toRet.Add(UpgradeItem.Empty);
            }
            else
            {
                toRet.Add(new UpgradeItem
                {
                    item = items[Random.Range(0, items.Count)],
                    count = Random.Range(1, UpgradeItem.maxCount) 
                });
            }
        }

        return toRet;
    }

    public List<UpgradeItem> GetEmptyItems(int count)
    {
        List<UpgradeItem> toRet = new List<UpgradeItem>();
        for (int i = 0; i < count; i++)
        {
            toRet.Add(UpgradeItem.Empty);
        }
        return toRet;
    }
}
