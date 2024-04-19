using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;   // Tham chiếu đến bảng điều khiển của inventory
    [SerializeField] GameObject toolbarPanel;
    // [SerializeField] GameObject statusPanel;
    // [SerializeField] GameObject additionalPanel;
    // [SerializeField] GameObject storePanel;

    private void Update()
    {
        // Người chơi bấm I sẽ hiện lên kho chứa
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!panel.activeInHierarchy)
                Open();
            else
                Close();
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        // statusPanel.SetActive(true);
        toolbarPanel.SetActive(false);
        // additionalPanel.SetActive(true);
        // storePanel.SetActive(false);
    }
    public void Close()
    {
        panel.SetActive(false);
        // statusPanel.SetActive(false);
        toolbarPanel.SetActive(true);
        // additionalPanel.SetActive(false);
        // storePanel.SetActive(false);
    }
}
