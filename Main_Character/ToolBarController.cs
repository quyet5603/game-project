using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;   // Giữ mã công cụ đang dùng
    public Action<int> onChange;

    public Item GetItem
    {
        get{
            return GameManager.instance.inventoryContainer.slots[selectedTool].item;
        }
    }
    internal void Set(int id)
    {
        // Lấy chỉ số ô được chọn
        selectedTool = id;
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        //VERIFICAR UMA FORMA MELHOR DE FAZER ISSO
        // if (Input.anyKeyDown)
        // {
        //     string inputTeclado = Input.inputString;
        //     if (!String.IsNullOrEmpty(inputTeclado))
        //     {
        //         foreach (char c in inputTeclado)
        //         {
        //             if (!char.IsDigit(c) || c == '0')
        //                 return;
        //         }

        //         selectedTool = Convert.ToInt32(inputTeclado) - 1;
        //         selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
        //         onChange?.Invoke(selectedTool);
        //     }            
        // }
            

        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool -= 1;
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);                
            }
            else
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }
}
