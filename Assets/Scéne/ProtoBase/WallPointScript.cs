using UnityEngine;

public class WallPointScript : MonoBehaviour
{
    public Tile linkedTile;


    public void Create(Tile tile)
    {
        linkedTile = tile;
        tile.currentWallPointScript = this;
        gameObject.transform.position = linkedTile.transform.position;
    }
}
