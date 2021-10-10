using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillScript : MonoBehaviour
{
    public float speed = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
        collision.rigidbody.velocity = collision.rigidbody.velocity * speed;
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<CollectionScript>().moveSpeed = 5.0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<CollectionScript>().moveSpeed = 10.0f;
        }
    }
}
