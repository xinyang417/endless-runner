using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  [SerializeField] float turnSpeed = 90f;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.GetComponent<Obstacle>() != null)
    {
      Destroy(gameObject);
      return;
    }

    // Check that the object we collided with is the player
    if (other.gameObject.name != "Player1")
    {
      return;
    }

    // Add to the player score
    GameManager.inst.IncrementScore();
    // Destroy this coin object
    Destroy(gameObject);
  }

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
  }
}