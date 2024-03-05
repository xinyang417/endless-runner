using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
  GroundSpawner groundSpawner;
  [SerializeField] GameObject coinPrefab;

  [SerializeField] GameObject obstaclePrefab;

  // Start is called before the first frame update
  void Start()
  {
    groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
  }

  private void OnTriggerExit(Collider other)
  {
    groundSpawner.SpawnTile(true);
    Destroy(gameObject, 2); // destroys the game object 2 seconds after the player leaves the trigger
  }
  
  public void SpawnObstacle()
  {
    // choose a random point to spawn the obstacle
    int obstacleSpawnIndex = Random.Range(2, 5);
    Transform spawnPoint = transform.GetChild((obstacleSpawnIndex)).transform;

    // spawn the obstacle at that position
    Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
  }
  
  public void SpawnCoins()
  {
    int coinsToSpawn = 10;
    for (int i = 0; i < coinsToSpawn; i++)
    {
      GameObject temp = Instantiate(coinPrefab, transform);
      temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
    }
  }

  Vector3 GetRandomPointInCollider(Collider collider)
  {
    Vector3 point = new Vector3(
      Random.Range(collider.bounds.min.x, collider.bounds.max.x),
      Random.Range(collider.bounds.min.y, collider.bounds.max.y),
      Random.Range(collider.bounds.min.z, collider.bounds.max.z)
    );
    if (point != collider.ClosestPoint(point))
    {
      point = GetRandomPointInCollider(collider);
    }

    point.y = 1;
    return point;
  }
}