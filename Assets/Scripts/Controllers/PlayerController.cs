using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Ship _ship;

    private PlayerInputs _inputs;

    private void Start()
    {
        _inputs = new PlayerInputs();
        if (_inputs == null)
            Debug.LogError("There is no PlayerInputs on the Player!!!!");
        _inputs.Ship.Enable();

        _ship = GameObject.Find("Player").gameObject.GetComponent<Ship>();
        if (_ship == null)
            Debug.LogError("There is no Ship for the PlayerController!!!!");
        InitializeInputs();
    }

    private void Update()
    {
        var rotation = _inputs.Ship.Turn.ReadValue<float>();
        _ship.RotateShip(rotation);
    }

    private void InitializeInputs()
    {
        _inputs.Ship.Anchor.performed += Anchor_performed;
        _inputs.Ship.SailsUp.performed += SailsUp_performed;
        _inputs.Ship.SailsDown.performed += SailsDown_performed;
        _inputs.Ship.SendDinghy.performed += SendDinghy_performed;
    }

    private void SendDinghy_performed(InputAction.CallbackContext obj)
    {
        _ship.DeployDinghy();
    }

    private void SailsDown_performed(InputAction.CallbackContext obj)
    {
        _ship.SetSails(true);
    }

    private void SailsUp_performed(InputAction.CallbackContext obj)
    {
        _ship.SetSails(false);
    }

    private void Anchor_performed(InputAction.CallbackContext obj)
    {
        _ship.SetAnchor();
    }
}


//[SerializeField]
//private Rigidbody2D _rb;
//private float _spriteRotationOffset = 0;
//Private float _rotationSpeed = 40f;
//private void LookAtMousePointer()
//{
//    var mouse = Mouse.current;
//    if (mouse == null)
//        return;

//    var mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
//    var direction = (Vector2)mousePos - _rb.position;
//    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _spriteRotationOffset;

//    var q = Quaternion.AngleAxis(angle, Vector3.forward);
//    _rb.transform.rotation = Quaternion.Slerp(_rb.transform.rotation, q, Time.deltaTime * _rotationSpeed);
//}