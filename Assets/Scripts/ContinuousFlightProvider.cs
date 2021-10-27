using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class ContinuousFlightProvider : MonoBehaviour
{

    public InputActionReference action;
    private float _speed;

    private void Update()
    {
        transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
    }

    private void HandleFlight(InputAction.CallbackContext context)
    {
        _speed = context.ReadValue<Vector2>().y * 2f;
    }

    private void HandleStopFlight(InputAction.CallbackContext context)
    {
        _speed = 0;
    }

    private void Awake()
    {
        action.action.performed += HandleFlight;
        action.action.canceled += HandleStopFlight;
    }

    private void OnDestroy()
    {
        action.action.performed -= HandleFlight;
        action.action.canceled -= HandleStopFlight;
    }
}
