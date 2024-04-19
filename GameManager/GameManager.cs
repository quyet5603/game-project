using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;   // Quản lý các tham chiếu đến các đối tượng trong game
    public GameObject player;
    public ItemContainer inventoryContainer;   // Tham chiếu đến khi nhặt item cho vào kho
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
    // public DialogueSystem dialogueSystem;
    // public PlaceableObjectsReferenceManager placeableObjects;
    // public ItemList ItemDB;
    // public OnScreenMessageSystem messageSystem;
    // public ScreenTint screenTint;

    private void Awake()
    {
        instance = this;
        // SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }
    
}
