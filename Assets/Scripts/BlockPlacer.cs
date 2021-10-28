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
    public InputActionReference gripPressed;
    
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
            
            
            _cross.transform.localScale = new Vector3(1, .01f, 1) * 
                Mathf.Pow((hit.point - transform.position).magnitude, 0.7f) / 30;
            
            
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
            HandleBlockPlaced(hit);
        if (hit.collider.gameObject.CompareTag("menuitem"))
            HandleMenuPressed(hit.collider.gameObject);
    }

    private void HandleGripPressed(InputAction.CallbackContext context)
    {
        if (!Raycast(out var hit))
            return;
        if (hit.collider.gameObject.CompareTag("block"))
            Destroy(hit.collider.gameObject);
    }

    private void HandleBlockPlaced(RaycastHit hit)
    {
        if (prefab == default)
            Destroy(hit.collider.gameObject);
        else
            Instantiate(prefab,
                hit.collider.gameObject.transform.position + hit.normal.normalized,
                hit.collider.gameObject.transform.rotation);
    }

    private void HandleMenuPressed(GameObject o)
    {
        var menuItem = o.GetComponent<MenuItem>();
        prefab = menuItem.itemPrefab;
    }

    private bool Raycast(out RaycastHit hit)
    {
        return Physics.Raycast(
            transform.position,
            transform.rotation * Vector3.forward,
            out hit);
    }

    private void Awake()
    {
        triggerPressed.action.started += HandleTriggerPressed;
        gripPressed.action.started += HandleGripPressed;
    }

    private void OnDestroy()
    {
        triggerPressed.action.started -= HandleTriggerPressed;
        gripPressed.action.started -= HandleGripPressed;
        if (_cross != default)
            Destroy(_cross);
    }
}