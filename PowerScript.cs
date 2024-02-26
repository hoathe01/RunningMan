using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    private float speed = 0.5f;
    private Coroutine _coroutine;
    private void Update()
    {
        _coroutine ??= StartCoroutine(nameof(ReverseMovement));
        transform.Rotate(Vector3.back * (60 * Time.deltaTime));
        transform.localPosition += Vector3.up * (speed * Time.deltaTime);
    }

    private IEnumerator ReverseMovement()
    {
        yield return new WaitForSeconds(0.75f);
        speed = -speed;
        _coroutine = null;
    }
    
}
