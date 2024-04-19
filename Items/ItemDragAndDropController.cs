using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] public ItemSlot itemSlot;   // Tham chiếu đến vật phẩm sẽ được kéo thả
    [SerializeField] GameObject itemIcon;
    RectTransform iconTransform;
    Image itemIconImage;

    private void Start()
    {
        itemSlot = new ItemSlot();   // Khởi tạo để đối tượng này không bao giờ null
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                // Kiểm tra nếu con trỏ đang không trỏ vào đối tượng UI nào
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    
                    // Sinh ra đối tượng ngay tại con trỏ chuột
                    ItemSpawnManager.instance.SpawnItem(worldPosition, itemSlot.item, itemSlot.count);
                    
                    itemSlot.Clear();   // Xóa dữ liệu slot kéo thả
                    itemIcon.SetActive(false);   // Hủy hình ảnh
                }
            }            
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        // Nếu chỗ để kéo vật phẩm ra khỏi kho chưa có gì
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);   // Sao chép trạng thái slot chuẩn bị kéo vào slot để kéo
            itemSlot.Clear();   // Xóa slot đang trong kho mà sắp được kéo ra
        }
        else
        {
            // Nếu item trong slot kéo thả giống với item trong slot được click
            if (itemSlot.item == this.itemSlot.item)
            {
                itemSlot.count += this.itemSlot.count;   // Tăng số lượng của slot được click
                this.itemSlot.Clear();   // Xóa dữ liệu slot kéo thả
            }
            else
            {
                // Lưu lại dữ liệu item mà vừa được click
                Item item = itemSlot.item;
                int count = itemSlot.count;
                itemSlot.Copy(this.itemSlot);   // Copy dữ liệu slot kéo thả vào slot được click
                this.itemSlot.Set(item, count);   // Slot kéo thả lấy thông tin của slot vừa được click
            }            
        }
        UpdateIcon();   // 

    }

    public void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            // Nếu không có gì trong slot kéo thả thì tắt icon
            itemIcon.SetActive(false);
        }
        else
        {        
            // Bật icon kéo thả và lấy ảnh của slot kéo thả    
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
        }
    }
}
