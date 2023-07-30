using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerEnemy : MonoBehaviour
{
    
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,waypoints[currentWaypointIndex].transform.position)<.1f){

            currentWaypointIndex++;
            Rotate180Degrees();

            if (currentWaypointIndex>=waypoints.Length){

                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed*Time.deltaTime);
    }

    private void Rotate180Degrees()
    {
        // Get the current rotation and add 180 degrees to the Y-axis
        Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(0f, -180f, 0f);

        // Set the new rotation
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
