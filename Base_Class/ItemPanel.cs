using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons; // Tham chiếu đến các nút

    public virtual void OnClick(int id) {}

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        // Khi kích hoạt inventory hoặc toolbar lên thì Show
        // Khi Show mới thực hiện cập nhật
        Show();
    }

    private void SetIndex()
    {
        // Duyệt qua kho chứa để đặt chỉ số cho các nút
        for(int i = 0 ; i < inventory.slots.Count && i < buttons.Count ; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public void Show()   // Đây là hàm cập nhật vật phẩm trên kho
    {
        // Duyệt qua các nút đặt hoặc ẩn dựa trên kho chứa
        for(int i = 0 ; i < inventory.slots.Count && i < buttons.Count ; i++)
        {
            if(inventory.slots[i].item == null)
            {
                // Nếu không thì xóa đi vật phẩm trong button này
                buttons[i].Clean();
            }
            else
            {
                // Nếu trong kho có vật phẩm thì đặt vào button
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
}
