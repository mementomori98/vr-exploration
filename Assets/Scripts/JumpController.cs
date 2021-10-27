using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class JumpController : MonoBehaviour
{

    public InputActionReference action;
    public GameObject rig;
    private float _speed;

    private void Update()
    {
        rig.transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
    }

    private void Awake()
    {
        action.action.performed += HandleJump;
        action.action.canceled += HandleStopJump;
    }

    private void OnDestroy()
    {
        action.action.performed -= HandleJump;
        action.action.canceled -= HandleStopJump;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        _speed = context.ReadValue<Vector2>().y;
    }

    private void HandleStopJump(InputAction.CallbackContext context)
    {
        _speed = 0;
    }
}
