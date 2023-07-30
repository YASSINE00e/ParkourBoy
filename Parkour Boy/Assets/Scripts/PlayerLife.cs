using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    bool dead = false;
    public bool dieAnimation1 = false;
    public bool dieAnimation2 = false;

    private void Update() {
        if(transform.position.y < -1f && !dead){
            Die();
            dieAnimation1 = true;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy Body")){
            dieAnimation2 = true;
            Die();
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovements>().enabled = false;
        }
    }

    void Die(){
        
        dead = true;
        deathSound.Play();
        Invoke(nameof(ReloadLevel),1.3f);
        
        
    }

    void ReloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
