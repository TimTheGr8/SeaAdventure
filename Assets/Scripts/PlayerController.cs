using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _moveSpeed = 10.0f;
    [SerializeField]
    private float _spriteRotationOffset = 0;

    private bool _shouldMoveForward = false;

    private void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            _shouldMoveForward = true;
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            _shouldMoveForward = false;
        }

        LookAtMousePointer();
    }

    private void FixedUpdate()
    {
        if (_shouldMoveForward)
        {
            _rb.velocity = -transform.up * _moveSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void LookAtMousePointer()
    {
        var mouse = Mouse.current;
        if (mouse == null)
            return;

        var mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        var direction = (Vector2)mousePos - _rb.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _spriteRotationOffset;

        _rb.rotation = angle;
    }
}
