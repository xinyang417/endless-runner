using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
  void Start()
  {
    // Check if a Collider is attached to this GameObject
    Collider collider = GetComponent<Collider>();
    if (collider != null)
    {
      Debug.Log("Collider is attached to this GameObject.");
    }
    else
    {
      Debug.Log("Collider is NOT attached to this GameObject.");
    }
  }
}