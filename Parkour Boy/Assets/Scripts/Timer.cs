using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int min = Mathf.FloorToInt(elapsedTime / 60); 
        int sec = Mathf.FloorToInt(elapsedTime % 60);    
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
