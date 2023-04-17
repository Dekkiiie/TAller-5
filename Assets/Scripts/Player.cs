using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{

    //Public vars
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 5f;
    public float airMoveSpeed = 7f;
   

    

    public float raycastDistance = 1.1f;
    public LayerMask groundMask;


    //Privates vars
    private float saveSpeed;
    private Rigidbody rb;
    private bool canDoubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        saveSpeed = movementSpeed;
    }

    void Update()
    {

       
       //Moverse
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementX = transform.right * horizontalInput * movementSpeed * Time.deltaTime;

        
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementZ = transform.forward * verticalInput * movementSpeed * Time.deltaTime;

        
        Vector3 movement = movementX + movementZ;
        rb.MovePosition(rb.position + movement);

        // Salto y Doble Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (IsGrounded())
            {
                movementSpeed = saveSpeed;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = true;
                
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
                canDoubleJump = false;
            }
            if (IsGrounded() == false)
            {
                movementSpeed = airMoveSpeed;
            }
        }

        //Verifica si esta tocando el piso segun la Capa
        bool IsGrounded()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            return Physics.Raycast(ray, raycastDistance, groundMask);
            
        }


    }

    
}

