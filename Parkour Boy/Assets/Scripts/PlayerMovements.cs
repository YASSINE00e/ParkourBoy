using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 7f;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Rigidbody rb;
    [SerializeField] float groudDrag = 10f;
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
    KeyCode jumpKey = KeyCode.Space;
    
    
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
            animator.SetBool("Running",true);
        }else{
            animator.SetBool("Running",false);
        }
        
        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCooldown);
        
            animator.SetBool("Jump",true);
        }
        else{
            animator.SetBool("Jump",false);
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
