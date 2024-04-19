using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tile Data")]
public class TileData : ScriptableObject
{
    // Liên kết ScriptableObject với tiles cho phép đào hoặc không trên gameplay
    public List<TileBase> tiles;
    public bool plowable;
}
