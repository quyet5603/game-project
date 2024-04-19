using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;
    int myIndex;


    public void SetIndex(int index)
    {
        myIndex = index;
    }
    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        // Cài đặt vật phẩm để hiển thị lên màn hình
        icon.sprite = slot.item.icon;
        // Kiểm tra vật phẩm có thể có nhiều số lượng
        if(slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else text.gameObject.SetActive(false);
    }
    public void Clean()
    {
        // Khi vật phẩm không còn trên Inventory nữa
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }

    public void Highlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    } 
}
