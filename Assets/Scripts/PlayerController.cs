using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 30f;

    private PlayerInputs _inputs;

    private void Awake()
    {
        _inputs = new PlayerInputs();
        if (_inputs == null)
            Debug.LogError("There is no RigidBody on the Player!!!!");

        _inputs.Ship.Enable();
    }

    private void Update()
    {
        var rotation = _inputs.Ship.Turn.ReadValue<float>();
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed * rotation);
    }
}


//[SerializeField]
//private Rigidbody2D _rb;
//private float _spriteRotationOffset = 0;
//private void LookAtMousePointer()
//{
//    var mouse = Mouse.current;
//    if (mouse == null)
//        return;

//    var mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
//    var direction = (Vector2)mousePos - _rb.position;
//    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _spriteRotationOffset;

//    _rb.rotation = angle;
//}