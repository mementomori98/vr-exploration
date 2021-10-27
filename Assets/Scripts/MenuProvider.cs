using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuProvider : MonoBehaviour
{
    public GameObject[] menuItemPrefabs;

    private GameObject[] _menuItems;

    public InputActionReference moveLeft;
    public InputActionReference moveRight;

    private Vector3[] _positions;

    private float[] _speeds = new float[5];

    private float _transitionDuration = 0.2f;
    private float _transitionStarted;

    private void Start()
    {
        _menuItems = menuItemPrefabs.Select(i => Instantiate(i, transform)).ToArray();
        _positions = new []{
            new Vector3(-3, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0.064f, 0, 0),
            new Vector3(0.128f, 0, 0),
            new Vector3(3, 0, 0),
        };
        ResetPositions();
    }

    private void Update()
    {
        if (_transitionStarted + _transitionDuration < Time.time)
            if (_speeds.Any(s => s != default))
            {
                _menuItems = _menuItems.Skip(1).Append(_menuItems[0]).ToArray();
                ResetPositions();
                return;
            }
            else
            {
                return;
            }

        for (var i = 0; i < _menuItems.Length; i++)
        {
            var direction = _positions[(i - 1) % _positions.Length] - _menuItems[i].transform.position;
            _menuItems[i].transform.position += direction * _speeds[i];
        }
    }

    private void HandleLeft(InputAction.CallbackContext context)
    {
        _menuItems = _menuItems.Skip(1).Append(_menuItems.Last()).ToArray();
        ResetPositions();
        // for (var i = 0; i < menuItems.Length; i++)
        // {
        //     _speeds[i] = (_positions[(i - 1) % 5] - menuItems[i].transform.position).magnitude / _transitionDuration;
        // }
        //
        // _transitionStarted = Time.time;
    }

    private void HandleRight(InputAction.CallbackContext context)
    {
        _menuItems = _menuItems.Skip(4).Concat(_menuItems.Take(4)).ToArray();
        ResetPositions();
        // for (var i = 0; i < menuItems.Length; i++)
        // {
        //     _speeds[i] = (_positions[(i + 1) % 5] - menuItems[i].transform.position).magnitude / _transitionDuration;
        // }
        //
        // _transitionStarted = Time.time;
    }

    private void ResetPositions()
    {
        for (var i = 0; i < _menuItems.Length; i++)
        {
            _menuItems[i].transform.position = _positions[i];
        }

        _speeds = new float[5];
    }

    private void Awake()
    {
        moveLeft.action.started += HandleLeft;
        moveRight.action.started += HandleRight;
    }

    private void OnDestroy()
    {
        moveLeft.action.started -= HandleLeft;
        moveRight.action.started -= HandleRight;
    }
}