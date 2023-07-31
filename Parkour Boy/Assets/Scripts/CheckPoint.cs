/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerLife pl;
    [SerializeField] List<GameObject> checkPoints;
    Vector3 vectorPoint;


    // Update is called once per frame
    void Update()
    {
        if(pl.dead){
            Invoke(nameof(Respown),1.3f);
        }
        
    }

    void Respown(){
        player.transform.position = vectorPoint;
        pl.dead=false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("CheckPoint")){
        vectorPoint = player.transform.position;
        }
    }
}
*/