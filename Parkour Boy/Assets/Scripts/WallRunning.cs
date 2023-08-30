using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]
    [SerializeField] LayerMask wall;
    [SerializeField] LayerMask ground;
    [SerializeField] float wallJumpUpForce;
    [SerializeField] float wallJumpSideForce;
    [SerializeField] float wallRunForce;
    [SerializeField] float maxWallRunTime;
    float wallRunTimer;

    [Header("Inputs")]
    float horizontalInput;
    float verticalInput;

    [Header("Detection")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float minJumpHit;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    bool wallLeft;
    bool wallRight;

    [Header("Exiting")]
    bool exitingWall;
    [SerializeField] float exitWallTime;
    float exitWallTimer;



    [Header("Referances")]
    [SerializeField] Transform orientation;
    PlayerMovements pm;
    Rigidbody rb;
    
    Animator animator;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovements>();
        animator=GetComponentInChildren<Animator>();

    }

    private void Update() {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate() {
        if(pm.wallrunning){
            WallRunningMovements();
        }
    }
    void CheckForWall(){
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, wall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wall);

    }

    bool AboveGround(){
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHit, ground);
    }

    void StateMachine(){
        //Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //State 1 WallRunning
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall){
            if(!pm.wallrunning){
                StartWallRun();
            }
            if(Input.GetKeyDown(jumpKey)){
                WallJump();
            }
        }
        //State 2 - Exiting
        else if(exitingWall){
            if(pm.wallrunning){
                StopWallRun();
            }
            if(exitWallTimer > 0){
                exitWallTimer -= Time.deltaTime; 
            }
            if(exitWallTimer <= 0){
                exitingWall = false;
            }
        }

        //State 3 - none
        else{
            if(pm.wallrunning){
                StopWallRun();
            }
        }
    }

    void StartWallRun(){
        pm.wallrunning = true;
    }
    void WallRunningMovements(){
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude){
            wallForward = -wallForward;
        }
        //froward froce
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //push to wall force
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && verticalInput < 0)){
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
    }
    void StopWallRun(){
        pm.wallrunning = false;
        rb.useGravity = true;
    }

    void WallJump(){
        //entre exiting wall state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;
        //reset y velocity and add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply,ForceMode.Impulse);
    }
}
