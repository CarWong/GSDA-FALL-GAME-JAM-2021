using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public GameObject target;
    private Vector3 _movementVector = new Vector3(1, 0, 1);
    private float _maxVelocity = 2.0f;
    private float _desiredVelocity;

    private float slowingRadius = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            SeekBehaviour();
            _movementVector = _movementVector * _desiredVelocity * Time.deltaTime;


            //Debug.Log(_movementVector);
            //Debug.Log(_desiredVelocity);

            transform.position += _movementVector;
        }
    }
    void SeekBehaviour()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        //Debug.Log(distance);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        _desiredVelocity = _desiredVelocity * _maxVelocity;

        _movementVector = (target.transform.position - transform.position).normalized;
    }
}
