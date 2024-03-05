using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
  [SerializeField] GameObject groundtile;

  private Vector3 nextSpawnPoint;

  // This function needs to be public for the GroundTile.cs script to be able to access the function
  public void SpawnTile(bool spawnItems)
  {
    // Instantiate is how you spawn an object in unity
    // The first input is the object you want to spawn
    // The second input where you want to spawn it
    // The third input: The rotation of it - this current value is no rotation
    GameObject temp = Instantiate(groundtile, nextSpawnPoint, Quaternion.identity);
    nextSpawnPoint = temp.transform.GetChild(1).transform.position;

    if (spawnItems)
    {
      temp.GetComponent<GroundTile>().SpawnObstacle();
      temp.GetComponent<GroundTile>().SpawnCoins();
    }
  }

  // Start is called before the first frame update
  private void Start()
  {
    for (int i = 0; i < 50; i++)
    {
      if (i < 3)
      {
        SpawnTile(false);
      }
      else
      {
        SpawnTile(true);
      }
    }
  }
}