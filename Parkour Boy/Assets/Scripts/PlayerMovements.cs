using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    Animator animator;
    public static bool IsJumping = false;
    //public static bool IsJumping = false;

    private Transform mainCameraTransform; // Reference to the main camera's transform

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator=GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform; // Get the main camera's transform
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        IsWalking();
        // Calculate movement direction relative to the camera's forward direction
        Vector3 cameraForward = mainCameraTransform.forward;
        cameraForward.y = 0f; // Ensure the cameraForward vector is horizontal (no vertical component)
        Vector3 movementDirection = cameraForward.normalized * verticalInput + mainCameraTransform.right * horizontalInput;
        movementDirection.y = 0f; // Ensure no vertical movement

        rb.velocity = movementDirection * movementSpeed + new Vector3(0f, rb.velocity.y, 0f);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            animator.SetBool("Jump",true);
        }else{
            animator.SetBool("Jump",false);
        }
        
    }

    void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();
        
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy Head")){
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    void IsWalking(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(horizontalInput != 0 || verticalInput !=0){
            animator.SetBool("Running",true);
        }else{
            animator.SetBool("Running",false);
        }
    }
}
