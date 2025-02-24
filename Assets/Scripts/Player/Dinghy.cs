using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dinghy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private float _maxSpeed = 2;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Land"))
        {
            //_rb.velocity = Vector2.zero;
            _speed = 0;
            StartCoroutine(GatherResources());
        }

        if (other.gameObject.CompareTag("Player"))
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
        _speed = _maxSpeed;
    }

    private void AddResources()
    {
        Destroy(this.gameObject);
    }

    // Choose how many different resources to collect
    private void ChooseResources()
    {
        Ship shipScript = _ship.GetComponent<Ship>();
        int resourceCount = Random.Range(1, shipScript.GetResourceCount() + 1);
        List<int> resourceType = new List<int>();
        bool resourceInList = false;
        // Choose what resources 
        while (resourceType.Count < resourceCount)
        {
            int rand = Random.Range(0, shipScript.GetResourceCount());
            foreach (int resource in resourceType)
            {
                if (resource == rand)
                {
                    resourceInList = true;
                    break;
                }
            }
            if (!resourceInList)
            {
                resourceType.Add(rand);
            }
            resourceInList = false;
        }
        // Choose the quantities of each resource
        foreach (int resource in resourceType)
        {
            int resourceQuantity = Random.Range(1, shipScript.GetResourceMax(resource) + 1);
            shipScript.AddResources(resource, resourceQuantity);

        }

    }

    IEnumerator GatherResources()
    {
        yield return new WaitForSeconds(_resourceGatherTime);
        ChooseResources();
        ReturnToShip();
    }
}

