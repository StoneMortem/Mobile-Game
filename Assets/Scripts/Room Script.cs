using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomScript : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;

    public float GetWidth()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        return bounds.size.x * groundTilemap.layoutGrid.cellSize.x;
    }
}
