using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    float bounceForce = 20f;
    public bool bounce = false;
    [SerializeField] AudioSource bounceSound;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bounceSound.Play();
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            bounce = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            bounce = false;
        }
    }
}
