using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemy;
    public Vector2 spawnBound;
    private int _enemyCount;
    private int _enemyStackUp;
    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        _enemyCount = GameObject.FindObjectsOfType<EnemyScript>().Length;
        if (_enemyCount == 0)
        {
            _enemyStackUp++;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _enemyStackUp; i++)
        {
            var enemyPos = new Vector3(
                Random.Range(spawnBound.x, spawnBound.y),
                enemy.transform.position.y + 0,
                Random.Range(spawnBound.x, spawnBound.y));
            var enemyRot = enemy.transform.rotation;
            Instantiate(enemy, enemyPos, enemyRot);   
        }
    }
}