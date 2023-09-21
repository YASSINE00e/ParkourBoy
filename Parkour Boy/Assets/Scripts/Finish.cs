using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
    
public class Finish : MonoBehaviour
{

    [SerializeField] AudioSource finishSound;
    [SerializeField] Text timerTextInGame;
    [SerializeField] Text timerText;
    [SerializeField] Text hiTimerText;
    [SerializeField] Text levelNumber;
    [SerializeField] GameObject panel;
    float elapsedTime;
    int timer;
    int hiTimer = 99999;

    private void Start() {
        if(PlayerPrefs.HasKey("HiTime"+(SceneManager.GetActiveScene().buildIndex-1))){
            hiTimer = PlayerPrefs.GetInt("HiTime"+(SceneManager.GetActiveScene().buildIndex-1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer = Mathf.FloorToInt(elapsedTime % 60);    
        timerTextInGame.text = timer.ToString();
    }
   private void OnTriggerEnter(Collider other) {
    if(other.gameObject.name=="Boy"){
        finishSound.Play();
        
        if(timer<hiTimer){
            hiTimer = timer;
            PlayerPrefs.SetInt("HiTime"+(SceneManager.GetActiveScene().buildIndex-1), hiTimer);
        }

        Time.timeScale = 0f;
        panel.SetActive(true);
        levelNumber.text = (SceneManager.GetActiveScene().buildIndex-1).ToString();
        
        timerText.text = "Time: " + timer.ToString();
        hiTimerText.text = "Hi-Time: " + hiTimer.ToString();

        UnlockedNewLevel();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
