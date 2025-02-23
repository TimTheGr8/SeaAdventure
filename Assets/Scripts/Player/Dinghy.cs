using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinghy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private float _resourceGatherTime = 1.0f;

    private GameObject _ship;
    private Vector3 _dinghyDestination;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool _returningToShip = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_rb == null )
            Debug.LogError($"The {gameObject.name} does not have a RigidBody2D!!!!");
        _collider = GetComponent<Collider2D>();
        if(_collider == null ) 
            Debug.LogError($"There is no Collider2D on the {gameObject.name}!!!!");
        _ship = GameObject.Find("Player");
        if (_ship == null)
            Debug.LogError("There is no ship for me to return to!!!!");
    }

    private void FixedUpdate()
    {
        if (_returningToShip)
            transform.LookAt(_ship.transform);

        _rb.velocity = (transform.forward) * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Land"))
        {
            _rb.velocity = Vector2.zero;
            StartCoroutine(GatherResources());
        }

        if(other.gameObject.CompareTag("Player"))
        {
            AddResources();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        destination.z = 0;
        _dinghyDestination = destination;
        transform.LookAt(_dinghyDestination);
    }

    private void ReturnToShip()
    {
        _returningToShip = true;
    }

    private void AddResources()
    {
        Destroy(this.gameObject);
    }

    IEnumerator GatherResources()
    {
        yield return new WaitForSeconds(_resourceGatherTime);
        ReturnToShip();
    }
}
