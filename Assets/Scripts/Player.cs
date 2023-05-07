using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField]private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponents;
    private int superJumpRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponents = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player jumps with space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }        

        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    // FixedUpdate is called once every physic update
    void FixedUpdate()
    {
        rigidbodyComponents.velocity = new Vector3(horizontalInput, rigidbodyComponents.velocity.y, 0);
        
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            return;
        }
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5;

            if (superJumpRemaining > 0) 
            {
                jumpPower *= 2;
                superJumpRemaining--;
            }
            
            rigidbodyComponents.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) 
        {
            Destroy(other.gameObject);
            superJumpRemaining++;
        }  
    }
}