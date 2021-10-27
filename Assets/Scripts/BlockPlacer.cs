using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockPlacer : MonoBehaviour
{
    public InputActionReference triggerPressed;
    public GameObject prefab;
    public GameObject crossPrefab;
    public Material selectMaterial;

    private GameObject _hoveredObject;
    private Material _originalMaterial;
    
    private GameObject _cross;
    
    private void Update()
    {
        if (Raycast(out var hit))
        {
            if (_hoveredObject != hit.collider.gameObject)
            {
                if (_hoveredObject != default)
                {
                    _hoveredObject.GetComponent<MeshRenderer>().material = _originalMaterial;
                    _hoveredObject = default;
                    _originalMaterial = default;
                }
                
                _hoveredObject = hit.collider.gameObject;
                _originalMaterial = _hoveredObject.GetComponent<MeshRenderer>().material;
                _hoveredObject.GetComponent<MeshRenderer>().material = selectMaterial;
            }

            var position = hit.point;
            var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            if (_cross == default)
                _cross = Instantiate(crossPrefab, position, rotation);
            else
            {
                _cross.transform.position = position;
                _cross.transform.rotation = rotation;
            }
        }
        else
        {
            if (_hoveredObject != default)
            {
                _hoveredObject.GetComponent<MeshRenderer>().material = _originalMaterial;
                _hoveredObject = default;
                _originalMaterial = default;
            }
            
            if (_cross != default)
                Destroy(_cross);
        }
        
    }

    private void HandleTriggerPressed(InputAction.CallbackContext context)
    {
        if (!Raycast(out var hit))
            return;
        
        if (hit.collider.gameObject.CompareTag("block"))
        {
            Instantiate(prefab,
                hit.collider.gameObject.transform.position + hit.normal.normalized,
                hit.collider.gameObject.transform.rotation);
        }
    }

    private bool Raycast(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out hit);
    }
    
    private void Awake()
    {
        triggerPressed.action.started += HandleTriggerPressed;
    }

    private void OnDestroy()
    {
        triggerPressed.action.started -= HandleTriggerPressed;
        if (_cross != default)
            Destroy(_cross);
    }
}