using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{

    //[SerializeField] PlayerMovements PlayerMovements;
    [SerializeField] PlayerLife PlayerLife;
    [SerializeField] WallRunning WallRunning;
    [SerializeField] JumpPad JumpPad;
    [SerializeField] PlayerMovements pm;
    [SerializeField] Vaulting vaulting;
    Animator animator;
    KeyCode jumpKey = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(PlayerMovements.IsWalking()){
            animator.SetBool("Running",true);
        }
        else{
            animator.SetBool("Running",false);
        }
        
        if(PlayerMovements.IsJumping){
            animator.SetBool("Jump",true);
        }
        else{
            animator.SetBool("Jump",false);
        }
        */

        if(PlayerLife.dieAnimation1){
            animator.SetTrigger("Die1");
            PlayerLife.dieAnimation1=false;
        }else{
            animator.ResetTrigger("Die1");
        }
        
        if(PlayerLife.dieAnimation2){
            animator.SetTrigger("Die2");
            PlayerLife.dieAnimation2=false;
        }else{
            animator.ResetTrigger("Die2");
        }

        if(WallRunning.wallLeft && pm.wallrunning){
            animator.SetBool("WallRunL",true);
        }else{
            animator.SetBool("WallRunL",false);
        }
        
        if(WallRunning.wallRight && pm.wallrunning){
            animator.SetBool("WallRunL",true);
        }else{
            animator.SetBool("WallRunR",false);
        }

        if(pm.wallrunning && Input.GetKey(jumpKey)){
            animator.SetBool("WallJump",true);
        }else{
            animator.SetBool("WallJump",false);
        }
/*
        if(vaulting.vaulting){
            animator.SetBool("Vault",true);
            vaulting.vaulting=false;
        }else{
            animator.SetBool("Vault",false);
        }

        
        if(JumpPad.bounce){
            animator.SetBool("Bounce",true);
        }else{
            animator.SetBool("Bounce",false);
        }
        */

        
    }
}
