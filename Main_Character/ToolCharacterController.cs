using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolCharacterController : MonoBehaviour
{
    CharacterController2D characterController2d;
    Rigidbody2D rgbd2d;
    ToolbarController toolbarController;
    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Character character;
    // ToolBarController toolBarController;
    // Animator animator;
    // [SerializeField] float delayUse = 0.1f;
    // private float delay = 0f;
    // //[SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;
    [SerializeField] ToolAction onTilePickUp;
    // [SerializeField] IconHighlight iconHighlight;
    // AttackController attackController;
    // [SerializeField] int weaponEnergyCost = 5;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        // Tham chiếu tới nhân vật để xác định cách tương tác vói môi trường
        // Nên đặt các lớp hoặc tham chiếu cần thiết vào Awake
        // Sau khi đã lấy được tất cả tham chiếu thì mới thực thi với đối tượng đó trong Start
        characterController2d = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        // toolBarController = GetComponent<ToolBarController>();
        // animator = GetComponent<Animator>();
        // attackController = GetComponent<AttackController>();
    }

    private void Update()
    {
        SelectTile();   // Chọn tile trước khi làm nổi bật
        CanSelectCheck();
        Marker(); // Làm nổi bật ô tile lên
        if(Input.GetMouseButtonDown(0))
        {
            if(UseTool()) return;
            UseToolGrid();
        }
    }

    private bool UseTool()
    {
        // Xác định tâm của vòng tròn trước mặt
        Vector2 position = rgbd2d.position + characterController2d.lastMotionVector * offsetDistance;
        // Trả về tất cả object trong vòng tròn đó
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach(Collider2D item in collider2Ds)
        {
            // Mỗi object lấy ra ToolHit (nếu có) để có thể gọi Hit()
            ToolHit hit = item.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                return true;
            }
        }
        return false;
    }

    // private void WeaponAction()
    // {
    //     Item item = toolBarController.GetItem;
    //     if (item == null)
    //         return;

    //     if (!item.isWeapon)
    //         return;

    //     EnergyCost(weaponEnergyCost);

    //     attackController.Attack(item.damage, characterController2d.lastMotionVector);

    // }

    // private void EnergyCost(int energyCost)
    // {
    //     character.GetTired(energyCost);
    // }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        // iconHighlight.cellPosition = selectedTile;
    }

    void CanSelectCheck()
    {
        // Nếu khoảng cách giữa chuột và player trong phạm vi thì cho phép chọn
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
        // iconHighlight.CanSelect = selectable;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + characterController2d.lastMotionVector * offsetDistance;

        Item item = toolBarController.GetItem;
        if (item == null)
        return false;
        if (item.onAction == null)
        return false;
        EnergyCost(item.onAction.energyCost);
        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);
        if (complete)
            {
                if (item.onItemUsed != null)
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }
        return complete;
    }

    private void UseToolGrid()
    {
        // Nếu ô tile có thể chọn
        if (selectable == true)
        {
            Item item = toolBarController.GetItem;
            if (item == null)
            {
                PickUpTile();
                return;
            }

            if (item.onTileMapAction == null)
                return;

            // EnergyCost(item.onTileMapAction.energyCost);

            animator.SetTrigger("act");
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController,item);

            if (complete)
            {
                if (item.onItemUsed != null)
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }
        }
    }

        private void PickUpTile()
        {
            if (onTilePickUp == null)
                return;
            onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
        }
}
