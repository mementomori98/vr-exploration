using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPlacer : MonoBehaviour
{

    public GameObject ray;

    // Update is called once per frame
    void Update()
    {
        ray.transform.position = transform.position;
        ray.transform.rotation = transform.rotation;
    }
}
