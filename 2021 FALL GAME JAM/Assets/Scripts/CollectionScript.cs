using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    public GameObject target;

    private float horizontal;
    private float forward;
    private float vertical;

    public bool isOnGround = true;
    public float jumpForce = 0.5f;
    public float gravityModifier = 1.0f;
    public float moveSpeed = 10.0f;

    private bool canpickup = false;
    private bool hasitem = false;

    private Vector3 handsOffset;

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        handsOffset = new Vector3(transform.localScale.x, 0, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        movementUpdate();
        Collect();
    }

    
    void movementUpdate()
    {
        horizontal = 0;
        forward = 0;
        vertical = 0;
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            horizontal -= Time.deltaTime*moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            horizontal += Time.deltaTime * moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
        {
            forward += Time.deltaTime * moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
        {
            forward -= Time.deltaTime * moveSpeed;
        }

        //transform.position += new Vector3(horizontal, 0, forward);
        playerRb.position += new Vector3(horizontal, 0, forward);
        //transform.position.Set(transform.position.x + horizontal, transform.position.y, transform.position.z+forward);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

    }
    void Collect()
    {
        if (canpickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                target.GetComponent<Rigidbody>().isKinematic = true;
                target.transform.position = transform.position + handsOffset;
                target.transform.parent = transform;
                hasitem = true;
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.Q) && hasitem)
        {
            hasitem = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.transform.parent = null;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.transform.CompareTag("Collection"))
        {
            canpickup = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        canpickup = false;
    }

    
}
