using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
   public float speed;
   public float maxHeight;
   private float _height = 0;
   private Coroutine _coroutine;
   private float _mode = 1;
   private IEnumerator Start()
   {
      enabled = false;
      yield return new WaitForSeconds(5);
      enabled = true;
   }

   private void Update()
   {
      
      if (_height >= maxHeight)
      {
         _coroutine ??= StartCoroutine(nameof(ReverseElevator));
      }
      else
      {
         var tmp = speed * Time.deltaTime;
         _height += tmp;
         transform.Translate(Vector3.up * (tmp * _mode));
      }
     

   }

   private IEnumerator ReverseElevator()
   {
      yield return new WaitForSeconds(3);
      _height = 0;
      _mode = -_mode;
      _coroutine = null;
   }
}
