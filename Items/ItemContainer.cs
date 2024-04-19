using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]   // Lớp này được sử dụng để lưu Item và số lượng tương ứng với 1 ô trên inventory
public class ItemSlot
{
    public Item item;
    public int count;   // Đếm cho các vật phẩm có số lượng

    public void Copy(ItemSlot slot)
    {
        // Thự hiện copy trạng thái từ 1 itemslot
        item = slot.item;
        count = slot. count;
    }
    public void Clear()
    {
        // Xóa dữ liệu 1 slot trong kho
        item = null;
        count = 0;
    }
    public void Set(Item item, int count)
    {
        // Thay đổi dữ liệu 1 slot
        this.item = item;
        this.count = count;
    }
}

// Lớp này cho phép tạo ra 1 đối tượng lưu dữ liệu vật phẩm chứ không được tham chiếu tới gameObject nào
[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Item item, int count = 1)
    {
        if(item.stackable == true)
        {
            // Tìm vật phẩm được thêm vào
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if(itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else // Thêm vật phẩm không thể xếp chồng
        {
            // Tìm ô trống đầu tiên trong item container
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if(itemSlot != null)
            {
                itemSlot.item = item;
            }
            // Nếu không còn slot trong itemcontainer thì vật phẩm sẽ không được thêm vào
        }
    }
    public void Remove(Item itemToRemove,int count = 1)
    {
        if(itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove)
            itemSlot.count -= count;
            if(itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else{
            while(count > 0){
                count -= 1;
                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null) return ;
                itemSlot.Clear();
            }
        }
    }
}
