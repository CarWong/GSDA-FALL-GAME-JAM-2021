using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    private Vector3 _movementVector = new Vector3(1, 0, 1);
    private float _maxVelocity = 5.0f;
    private float _desiredVelocity;

    private float slowingRadius = 2.0f;

    private bool canpickup = false;
    private bool hasitem = false;

    private bool playerCollide = false;

    private Vector3 handsOffset;

   
    // Start is called before the first frame update
    void Start()
    {
        handsOffset = new Vector3(transform.localScale.x, 0, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Collect();
            if (!hasitem || player.transform.GetComponent<CollectionScript>().isItemEquipped())
            {
                SeekBehaviour();
            }
            else
            {
                FleeBehaviour();
                
            }
            _movementVector = _movementVector * _desiredVelocity * Time.deltaTime;


            //Debug.Log(_movementVector);
            //Debug.Log(_desiredVelocity);

            transform.position += _movementVector;
        }
    }
    void Collect()
    {
        if (canpickup)
        {
            target.GetComponent<Rigidbody>().isKinematic = true;
            target.transform.position = transform.position + handsOffset;
            target.transform.parent = transform;
            hasitem = true;
        }
        if (playerCollide)
        {
            hasitem = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.transform.parent = null;
            //Freeze();
        }
    }
    public void setItem(bool state)
    {
        hasitem = state;
        canpickup = state;
    }
    void Jump()
    {
        transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
        
    }
    void SeekBehaviour()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        //Debug.Log(distance);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        _desiredVelocity = _desiredVelocity * _maxVelocity;

        _movementVector = (target.transform.position - transform.position).normalized;
    }
    private void FleeBehaviour()
    {
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        float distance = (player.transform.position - transform.position).magnitude;
        //Debug.Log(distance);
        _desiredVelocity = (player.transform.position - transform.position).normalized.magnitude;

        //quicker the closer we are to the target
        _desiredVelocity = _desiredVelocity * _maxVelocity * (slowingRadius / distance);

        _desiredVelocity *= -1; //flip direction

        _movementVector = (player.transform.position - transform.position).normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Collection"))
        {
            canpickup = true;
        }
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Enemy"))
        {
            playerCollide = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        playerCollide = false;
        canpickup = false;
    }
}
