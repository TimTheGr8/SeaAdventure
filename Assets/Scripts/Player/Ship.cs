using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField]
    private GameObject _dinghy;
    [SerializeField]
    private Transform _deployPosition;

    private bool _anchorDown = false;
    private bool _sailsDown = false;
    private float _currentSpeed = 0f;
    private float _destinationSpeed = 0f;
    private Dinghy _dinghyScript;
    
    void Start()
    {
        if (_rb == null)
            Debug.LogError($"There is no RigidBody on the {gameObject.name}!!!!");
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

    public void DeployDinghy()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.tag == "Land")
        {
            GameObject deployedDinghy = Instantiate(_dinghy, _deployPosition.position, Quaternion.identity);
            _dinghyScript = deployedDinghy.GetComponent<Dinghy>();
            // Checking if dinghy script is null
            if (_dinghyScript == null)
                Debug.LogError($"There is not Dinghy script on the {gameObject.name}!!!!");
           else 
                _dinghyScript.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
