using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    public CropsManager cropsManager;
    [SerializeField] Tilemap tilemap;
    // public CropsManager cropsManager;   
    // public PlaceableObjectsReferenceManager objectsManager;

    private void Start()
    {
        // Tạo từ điển khi tương tác với tile
        // Với mỗi loại tile thì sẽ thêm các khối ví dụ vào
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }
    

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
    {
        // if (tilemap == null)        
        //     tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        // if (tilemap == null)
        //     return Vector3Int.zero;

        Vector3 worldPosition;

        if (mousePosition)
        {
            // Chuyển vị trí chuột về vị trí trong thế giới game
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else worldPosition = position;
        // Chuyển vị trí thế giới game về vị trí cell
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (tilemap == null)
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        if (tilemap == null)
            return null;
        // Lấy tile theo vị trí cell
        TileBase tile = tilemap.GetTile(gridPosition);
        return tile;
    }

}
