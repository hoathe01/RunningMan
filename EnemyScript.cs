using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   public float speed;
   private GameObject _player;
   private Vector3 _goToward;
   private Rigidbody _rigidBody;
   private void Awake()
   {
      _player = GameObject.FindGameObjectWithTag("Player");
      _rigidBody = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      _goToward = _player.transform.position - transform.position;
      _goToward.Normalize();
      _goToward.y = 0;
      _rigidBody.AddForce(_goToward * speed);
      limitDepth(-15);
   }

   public void limitDepth(float depth)
   {
      if (transform.position.y <= depth)
      {
         Destroy(gameObject);
      }
   }
}
