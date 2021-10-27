using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlacer : MonoBehaviour
{
    
    public GameObject menu;
    
    private void Update()
    {
        menu.transform.position = transform.position;
        menu.transform.rotation = transform.rotation;
    }
}
