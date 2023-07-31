using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{

    [SerializeField] PlayerMovements PlayerMovements;
    [SerializeField] PlayerLife PlayerLife;
    [SerializeField] JumpPad JumpPad;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        /*
        if(JumpPad.bounce){
            animator.SetBool("Bounce",true);
        }else{
            animator.SetBool("Bounce",false);
        }
        */

        
    }
}
