using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public bool readyToRun;
    public bool isRunning;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundDrag;
    public Transform orientation;
    public LayerMask whatIsGround;
    public bool grounded;

    float horizontalInput; 
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        MovePlayer();

        //handle drag
        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }

        //check for sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            isRunning = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)){
            isRunning = false;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction -- ensures that the player is always walking in the direction their facing.
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
           //on ground
           if (isRunning == true){
               rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force);
           } else{
               rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
           }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit moveSpeed velocity if needed
        if (flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y,  limitedVel.z);
        } 

        //limit sprintSpeed velocity if needed
        if (flatVel.magnitude > sprintSpeed){
            Vector3 limitedVel = flatVel.normalized * sprintSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y,  limitedVel.z);
        }
    } 
}
