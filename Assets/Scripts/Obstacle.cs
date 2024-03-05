using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  private PlayerMovement playerMovement;

  void Start()
  {
    // looks for an object of type Player Movement
    playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "Player1")
    {
      // kill the player
      playerMovement.Die();
    }
  }

  // Update is called once per frame
  void Update()
  {
  }
}