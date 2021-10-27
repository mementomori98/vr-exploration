using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    public GameObject grass;

    void Start()
    {
        for (var x = -10.0f; x < 11.0f; x += 1.0f)
        {
            for (var z = -10.0f; z < 11.0f; z += 1.0f)
            {
                Instantiate(grass, new Vector3(x, -0.5f, z), Quaternion.identity);
            }
        }
    }
}