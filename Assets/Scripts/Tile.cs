using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSpawner spawner;
    public void DestroyTile()
    {
        if (spawner != null)
        {
            spawner.IncreaseScore();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("TileSpawner not assigned in Tile prefab!");
        }
    }
}
