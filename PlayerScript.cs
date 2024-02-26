using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;

    private Rigidbody _rigidBody;
    private GameObject _camera;
    private Vector3 _cameraOffset;
    private GameObject _elevator;
    private float _elevatorOffsetY;
    private float _speedModifier;

    private void Awake()
    {
        var circleObj = new GameObject
        {
            name = "Circle",
            transform =
            {
                parent = transform
            }
            
        };
        var circlePos = Vector3.zero;
        circleObj.transform.localPosition = circlePos;
        
        _rigidBody = GetComponent<Rigidbody>();
        _speedModifier = 1;
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraOffset = transform.position - _camera.transform.position;
    }

    private void Update()
    {
        var horizontalInp = Input.GetAxis("Horizontal");
        var verticalInp = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontalInp, 0, verticalInp);
        var direction = Quaternion.LookRotation(movement);
        var playerPos = _rigidBody.position;

        movement.Normalize();
        if (_elevator)
        {
            playerPos.y = _elevator.transform.parent.position.y + _elevatorOffsetY;
        }

        movement *= _speedModifier * speed * Time.deltaTime;

        direction = Quaternion.RotateTowards(
            transform.rotation,
            direction,
            360 * Time.fixedDeltaTime);

        _rigidBody.MovePosition(playerPos + movement);

        if (movement == Vector3.zero) return;
        _rigidBody.MoveRotation(direction);
    }


    private void LateUpdate()
    {
        _camera.transform.position = transform.position - _cameraOffset;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Power"))
        {
            Destroy(other.gameObject);
            _speedModifier = 2;
            StartCoroutine(nameof(BonusSpeedCountDown));
        }

        if (other.gameObject.CompareTag("Enemy") && _speedModifier >1)
        {
            var enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = other.transform.position - transform.position;
            enemyRigidBody.AddForce(awayFromPlayer * 50,ForceMode.Impulse);
        }
    }

    private IEnumerator BonusSpeedCountDown()
    {
        yield return new WaitForSeconds(3);
        _speedModifier = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            _elevator = other.gameObject;
            _elevatorOffsetY = transform.position.y - _elevator.transform.parent.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            _elevator = null;
            _elevatorOffsetY = 0;
        }
    }
}