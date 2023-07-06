using UnityEngine;
using UnityEngine.Tilemaps;

public class PerspectiveScript : MonoBehaviour
{
    public Renderer playerRenderer;
    public Tilemap[] tilemaps;
    public int playerSortingOrder = 0;
    public string tilesSortingLayer = "Default";
    public int tilesSortingOrderBelow = -1;
    public int tilesSortingOrderAbove = 1;
    public Vector2 detectionOffset = Vector2.zero;

    private void LateUpdate()
    {
        Vector3 playerPosition = playerRenderer.transform.position + (Vector3)detectionOffset;
        int tileSortingOrder = playerSortingOrder;
        bool foundTile = false;

        foreach (Tilemap tilemap in tilemaps)
        {
            Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);
            Vector3 playerCellCenter = tilemap.GetCellCenterWorld(playerCellPosition);

            // Calculate the position within the tile
            Vector3 tilePosition = playerPosition - playerCellCenter;

            TileBase tile = tilemap.GetTile(playerCellPosition);

            if (tile != null)
            {
                TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();

                if (tilePosition.y > 0)
                    tileSortingOrder += tilesSortingOrderAbove;
                else
                    tileSortingOrder += tilesSortingOrderBelow;

                tilemapRenderer.sortingLayerName = tilesSortingLayer;
                tilemapRenderer.sortingOrder = tileSortingOrder;
                foundTile = true;

                // Manually invalidate the tilemap's cache for the updated tile
                tilemap.RefreshTile(playerCellPosition);
            }
        }

        if (!foundTile)
        {
            // If no tile is found, set the sorting order based on the playerSortingOrder
            playerRenderer.sortingOrder = playerSortingOrder;
        }
        else
        {
            // Set the sorting order of the player based on the calculated tileSortingOrder
            playerRenderer.sortingOrder = tileSortingOrder;
        }
    }
}
