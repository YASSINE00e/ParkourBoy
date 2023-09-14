using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class Finish : MonoBehaviour
{

    //[SerializeField] AudioSource finishSound;
   private void OnTriggerEnter(Collider other) {
    if(other.gameObject.name=="Boy"){
        //finishSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UnlockedNewLevel();
    }
   }

   void UnlockedNewLevel(){
    if(SceneManager.GetActiveScene().buildIndex>=PlayerPrefs.GetInt("ReachedIndex")){
        PlayerPrefs.SetInt("ReachedIndex",SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.SetInt("UnlockedLevel",PlayerPrefs.GetInt("UnlockedLevel",1)+1);
        PlayerPrefs.Save();

    }
   }
}
