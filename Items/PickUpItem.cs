using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;   // Lưu vị trí player
    [SerializeField] float spped = 5f;   // Tốc độ di chuyển vật phẩm về phía player
    [SerializeField] float pickUpDistance = 1.5f; // Khoảng cách đủ để nhặt
    [SerializeField] float ttl = 120f;   // time to live - Thời gian tồn tại của item (s)

    public Item item;   // Tham số khi Add() vật phẩm nhặt được vào kho
    public int count = 1;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(this.gameObject);
        }

        // Lưu khoảng cách từ vật phẩm tới player
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if(distance > pickUpDistance) return;
        // Nếu trong phạm vi thì di chuyển về phía player
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, spped * Time.deltaTime);
        

        if(distance < 0.1f)
        {
            // Nên được đưa vào controller cụ thể hơn là kiểm tra tại đây
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);              
            }
            else
            {
                Debug.LogWarning("Khong thay kho chua");
            }
            Destroy(this.gameObject);   // Đến gần player thì hủy object
        }
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
        transform.localScale = new Vector3(-1.5f, -1.5f, 1f);   // Customize
    }
    
}
