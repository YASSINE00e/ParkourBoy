using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Vaulting : MonoBehaviour
{
    // Start is called before the first frame update
    private int vaultLayer;
    [SerializeField] Transform orientation;
    private float playerHeight = 3.5f;
    private float playerRadius = 0.3f;
    public bool vaulting;
    Animator animator;
    void Start()
    {
        vaultLayer = LayerMask.NameToLayer("VaultLayer");
        vaultLayer = ~vaultLayer;
        animator=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vault();
    }
    private void Vault()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        //if(CrossPlatformInputManager.GetButtonDown("Jumpp"))
        {
            if (Physics.Raycast(orientation.position, orientation.forward, out var firstHit, 1f, vaultLayer))
            {
                print("vaultable in front");
                
                if (Physics.Raycast(firstHit.point + (orientation.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
                {
                    print("found place to land");
                    StartCoroutine(LerpVault(secondHit.point, 0.5f));

                    }/*
                    vaulting = true;
                    animator.SetBool("Vault",true);

                }
                else{
                    vaulting = false;
                    animator.SetBool("Vault",false);
                }*/
            }
        }
        

    }
    IEnumerator LerpVault(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}