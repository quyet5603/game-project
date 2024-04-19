using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quản lý tương tác của những hộp đồ khi player nhặt
public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    public override void Interact(Character character)
    {
        // Khi không mở thì cho phép lấy items
        if(opened == false)
        {
            opened = true;
            closedChest.SetActive(false);
            openedChest.SetActive(true);
        }
    }
}
