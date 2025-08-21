using Nova;
using UnityEngine;
using System.Collections.Generic;


public class UpgradesPanel : MonoBehaviour
{
    public ItemDatabase ItemDatabase = null;

    [Header("Upgrades")]
    public GridView UpgradeGrid = null;
    public int UpgradeCount = 24;

    [Header("Row Styling")]
    public RadialGradient RowGradient;

    [HideInInspector]
    private List<UpgradeItem> UpgradeItems = null;

    private void Start()
    {
        UpgradeItems = ItemDatabase.GetEmptyItems(UpgradeCount);

        InitGrid(UpgradeGrid, UpgradeItems);
    }

    public void AddItemToUpgradeInventory(UpgradeDescription itemDesc, int count = 1)
    {
        var existing = UpgradeItems.Find(x => x.item == itemDesc);
        if (existing != null)
        {
            existing.count = Mathf.Min(existing.count + count, UpgradeItem.maxCount);
        }
        else
        {
            int emptyIndex = UpgradeItems.FindIndex(x => x.isEmpty);
            if (emptyIndex != -1)
            {
                UpgradeItems[emptyIndex] = new UpgradeItem
                {
                    item = itemDesc,
                    count = count
                };
            } 
        }


        if (UpgradeGrid.gameObject.activeInHierarchy)
        {
            UpgradeGrid.Refresh();
        }
    }

    private void InitGrid(GridView grid, List<UpgradeItem> datasource)
    {

        grid.AddDataBinder<UpgradeItem, UpgradeItemVisuals>(BindItem);

        grid.SetSliceProvider(ProvideSlice);

        grid.AddGestureHandler<Gesture.OnHover, UpgradeItemVisuals>(UpgradeItemVisuals.HandleHover);
        grid.AddGestureHandler<Gesture.OnUnhover, UpgradeItemVisuals>(UpgradeItemVisuals.HandleUnhover);
        grid.AddGestureHandler<Gesture.OnPress, UpgradeItemVisuals>(UpgradeItemVisuals.HandlePress);
        grid.AddGestureHandler<Gesture.OnRelease, UpgradeItemVisuals>(UpgradeItemVisuals.HandleRelease);

        grid.SetDataSource(datasource);
    }

    private void ProvideSlice(int sliceIndex, GridView gridview, ref GridSlice2D gridslice)
    {
        gridslice.Layout.AutoSize.Y = AutoSize.Shrink;
        gridslice.AutoLayout.AutoSpace = true;
        gridslice.Layout.Padding.Value = 30;

        gridslice.Gradient = RowGradient;
    }
    private void BindItem(Data.OnBind<UpgradeItem> evt, UpgradeItemVisuals target, int index) => target.Bind(evt.UserData);
}
