using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomScript : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    private Transform enemy;

    // SETTERS
    public void SetEnemy(Transform enemy) { this.enemy = enemy; }

    // GETTERS
    public Transform GetEnemy() { return enemy; }
    public float GetWidth()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        return bounds.size.x * groundTilemap.layoutGrid.cellSize.x;
    }
}
