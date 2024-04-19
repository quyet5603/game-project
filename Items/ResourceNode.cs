using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;

    [SerializeField] Item item;
    [SerializeField] int dropCount = 5;
    [SerializeField]int itemCountInOneDrop;

    private void Awake()
    {
        itemCountInOneDrop = Random.Range(1,5);
    }

    public override void Hit()
    {
        while(dropCount > 0)
        {
            dropCount--;
            Vector3 position = this.transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2.0f;
            position.y += spread * UnityEngine.Random.value - spread / 2.0f;

            ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }
        // Khi bị tác động thì xóa gameobject đi
        Destroy(gameObject);
    }
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.contain
    }
}
