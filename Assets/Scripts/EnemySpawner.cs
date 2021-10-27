using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private float _lastSpawned;

    void Update()
    {
        if (Time.time - _lastSpawned < 5.0f)
            return;

        Instantiate(enemy, new Vector3(7f, 1.1f, -.5f), Quaternion.identity);
        _lastSpawned = Time.time;
    }
}