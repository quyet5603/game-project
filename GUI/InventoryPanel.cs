using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        // Khi click vào button trong kho chứa sẽ cho phép kéo thả != ToolBar
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        // CLick xong là phải cập nhật bảng kho chứa luôn
        Show();
    }
}
