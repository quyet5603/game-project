using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector2Int position;
    public bool complete{
        get{
            if(crop == null) return false;
            return growTimer >= crop.timeToGrow;
        }
    }
    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    // Lưu thông tin vị trí các pp đất đã trồng
    [SerializeField] GameObject cropsSpritePrefab;
    Dictionary<Vector2Int, Crops>  crops;
    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        OnTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        foreach (CropTile cropTile in crops.Values)
        {
            if(cropTile.crop == null){ continue; }
            cropTile.damage += 0.02f;
            if(cropTile.damage > 1f)
            {
                cropTile.Harvested();
                targetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }
            if(cropTile.Complete)
            {
                Debug.Log("I'm done growing");
                continue;
            }
            cropTile.growTimer += 1;
            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                cropTile.growStage += 1;
            }
           
        }
    }
    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }
    public void Plow(Vector3Int position)
    {
        // Nếu vị trí đã có ô đất được trồng
        if (crops.ContainsKey((Vector2Int)position)) return;
        CreatePlowedTile(position);
    }
    public void Seed(Vector3Int position,CropsManager toSeed)
    {
        // CropTile tile = container.Get(position);

        // if (tile == null)
        //     return;

        targetTilemap.SetTile(position, seeded);
        crops[(Vector2Int)position].crop = toSeed;

        // tile.crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        // Lưu vị trí được trồng
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();
        crop.position = position;
        // Hiển thị ô đất được cày lên
        targetTilemap.SetTile(position, plowed);
    }

   internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if(crops.ContainsKey(position) == false){
            return;
        }
        CropTile cropTile = crops[position]
        if(cropTile.Complete)
        {
            ItemSpawnManger.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition),cropTile.crop.yield,cropTile.crop.count);
            targetTilemap.SetTile(gridPosition,plowed);
            cropTile.Harvested();
        }
    }
}
