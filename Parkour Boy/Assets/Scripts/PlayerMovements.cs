using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    [Header("Movement")]
    float moveSpeed = 7f;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float wallRunSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Rigidbody rb;
    [SerializeField] float groudDrag = 12f;
    [SerializeField] Transform orientation;

    [Header("Ground Check")]
    [SerializeField] LayerMask ground;
    bool grounded;
    
    
    

    [Header("Jump")]
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float jumpCooldown = 0.25f;
    [SerializeField] float airMultiplier = 0.4f;
    bool readyToJump;
    
    [SerializeField] Transform groundCheck;
    
    [SerializeField] AudioSource jumpSound;
    Animator animator;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField] MovementState state;

    public enum MovementState{
        walking,
        sprinting,
        wallrunning,
        air
    }

    public bool wallrunning;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator=GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.CheckSphere(groundCheck.position, .1f, ground);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if(grounded){
            rb.drag = groudDrag;
        }else{
            rb.drag = 0f;
        }
    }
    private void FixedUpdate() {
        MovePlayer();
    }

    void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(horizontalInput != 0 || verticalInput !=0){
            animator.SetBool("Walking",true);
        }else{
            animator.SetBool("Walking",false);
        }
        
        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCooldown);}
        
        /*    animator.SetBool("Jump",true);
        }
        else{
            animator.SetBool("Jump",false);
        }*/
    }

    void StateHandler(){
        //Mode - WallRunning
        if(wallrunning){
            state = MovementState.wallrunning;
            moveSpeed = wallRunSpeed;
        }

        //Mode - sprinting 
        if(grounded && Input.GetKey(sprintKey)){
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        //Mode - Walking 
        else if(grounded){
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else{
            state = MovementState.air;
        }
    }
    void MovePlayer(){

        //calculate movement direction
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // in air
        else if(!grounded)
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump(){
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
        jumpSound.Play();
        
    }

    void ResetJump(){
        readyToJump = true;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy Head")){
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

}
