using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public Vector3 direction;

    public void HandleSelected(SelectEnterEventArgs e)
    {
        if (e.interactor.CompareTag("builder"))
            Instantiate(prefab,
                parent.transform.position + direction,
                Quaternion.identity);
        if (e.interactor.CompareTag("destroyer"))
            Destroy(parent);
    }

    public void HandleSelectedSmart(SelectEnterEventArgs e)
    {
        
    }
}