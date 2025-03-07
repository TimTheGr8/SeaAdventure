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
    [SerializeField]
    Ship _shipScript;
    [SerializeField]
    int _resourceCount = 0;
    [SerializeField]
    private List<int> _resourceType = new List<int>();
    [SerializeField]
    private List<int> _resourceLootTable = new List<int> { 60, 25, 15, 5 };
    [SerializeField]
    private int _resourceQuantity = 0;

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
            Debug.LogError($"{gameObject.name} has no ship for me to return to!!!!");

        _shipScript = _ship.GetComponent<Ship>();
        if(_shipScript  == null)
            Debug.LogError($"There is no Ship Script on the {gameObject.name}!!!!");
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
            _speed = 0;
            StartCoroutine(GatherResources());
        }

        if (other.gameObject.CompareTag("Player") && _shipScript != null)
        {
            // Choose the quantities of each resource
            foreach (int resource in _resourceType)
            {
                _resourceQuantity = Random.Range(1, _shipScript.GetResourceMax(resource) + 1);
                _shipScript.AddResources(resource, _resourceQuantity);

            }
            _shipScript.DinghyReturn();

            Destroy(this.gameObject);
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

    // Choose how many different resources to collect
    private void ChooseResources()
    {
        bool resourceInList = false;
        _resourceCount = RandomInt(true);
        // Choose what resources 
        while (_resourceType.Count < _resourceCount)
        {
            int rand = RandomInt(false);
            foreach (int resource in _resourceType)
            {
                if (resource == rand)
                {
                    resourceInList = true;
                    break;
                }
            }
            if (!resourceInList)
            {
                _resourceType.Add(rand);
            }
            resourceInList = false;
        }
    }

    private int RandomInt(bool isResourceCount)
    {
        float rand = Random.Range(0f, 1f);
        if (isResourceCount)
        {
            switch (rand)
            {
                case float n when (n > 0f && n <= 0.45f):
                    return 1;
                case float n when (n > 0.46f && n <= 0.65f):
                    return 2;
                case float n when (n > 0.66f && n <= 0.85f):
                    return 3;
                default:
                    return 4;
            }
        }
        else
        {
            switch (rand)
            {
                case float n when (n > 0f && n <= 0.5f):
                    return 0;
                case float n when (n > 0.51f && n <= 0.70f):
                    return 1;
                case float n when (n > 0.71f && n <= 0.85f):
                    return 2;
                default:
                    return 3;
            }
        }
    }

    IEnumerator GatherResources()
    {
        yield return new WaitForSeconds(_resourceGatherTime);
        ChooseResources();
        ReturnToShip();
    }
}

