using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 30f;
    [SerializeField]
    private float _moveSpeed = 10f;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private GameObject _main_Sail_Down;
    [SerializeField]
    private GameObject _main_Sail_Up;

    private bool _anchorDown = false;
    private bool _sailsDown = false;
    private float _currentSpeed = 0f;
    private float _destinationSpeed = 0f;
    
    void Start()
    {
        if (_rb == null)
            Debug.LogError("There is no RigidBody on the ship!!!!");
    }

    void FixedUpdate()
    {
        if(_anchorDown)
        {
            _rb.velocity = Vector2.zero;
            _currentSpeed = 0f;
            _destinationSpeed = 0f;
            return;
        }

        _rb.velocity = (transform.up * -1) * _currentSpeed;
    }

    public void RotateShip(float rotation)
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed * rotation);
    }

    public void SetSails(bool sailsDown)
    {
        _sailsDown = sailsDown;
        if(_sailsDown)
        {
            _main_Sail_Up.SetActive(false);
            _main_Sail_Down.SetActive(true);
            _destinationSpeed = _moveSpeed;
        }
        else
        {
            _main_Sail_Down.SetActive(false);
            _main_Sail_Up.SetActive(true);
            _destinationSpeed = 0;
        }
        StartCoroutine(SetShipSpeed());
    }

    public void SetAnchor()
    {
        _anchorDown = !_anchorDown;
        if(!_anchorDown && _sailsDown)
        {
            _destinationSpeed = _moveSpeed;
            StartCoroutine(SetShipSpeed());
        }
    }

    IEnumerator SetShipSpeed()
    {
        while (_currentSpeed != _destinationSpeed)
        {
            yield return new WaitForSeconds(0.1f);
            if (_destinationSpeed > 0)
            {
                _currentSpeed += 0.1f;
                if (_currentSpeed > _destinationSpeed)
                {
                    _currentSpeed = _moveSpeed;
                }
            }
            else if (_destinationSpeed == 0 && _currentSpeed > 0)
            {
                _currentSpeed -= 0.1f;
                if (_currentSpeed < _destinationSpeed)
                {
                    _currentSpeed = 0;
                }
            }
        }
    }
}
