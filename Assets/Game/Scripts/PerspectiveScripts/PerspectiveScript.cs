using UnityEngine;
using UnityEngine.Tilemaps;

public class PerspectiveScript : MonoBehaviour
{
    public Renderer playerRenderer;
    public Tilemap tilemap;
    public int playerSortingOrder = 0;
    public string tilesSortingLayer = "Default";
    public int tilesSortingOrderBelow = -1;
    public int tilesSortingOrderAbove = 1;

    private void LateUpdate()
    {
        Vector3 playerPosition = playerRenderer.transform.position;
        Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);
        Vector3 playerCellCenter = tilemap.GetCellCenterWorld(playerCellPosition);

        // Calculate the position within the tile
        Vector3 tilePosition = playerPosition - playerCellCenter;

        TileBase tile = tilemap.GetTile(playerCellPosition);

        if (tile != null)
        {
            TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
            int tileSortingOrder;

            if (tilePosition.y > 0)
                tileSortingOrder = playerSortingOrder + tilesSortingOrderAbove;
            else
                tileSortingOrder = playerSortingOrder + tilesSortingOrderBelow;

            tilemapRenderer.sortingLayerName = tilesSortingLayer;
            tilemapRenderer.sortingOrder = tileSortingOrder;
            playerRenderer.sortingOrder = playerSortingOrder;

            // Manually invalidate the tilemap's cache for the updated tile
            tilemap.RefreshTile(playerCellPosition);
        }
    }
}
