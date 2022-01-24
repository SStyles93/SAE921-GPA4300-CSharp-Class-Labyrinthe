using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float groundingTime = 1.0f;
    public bool startGrounding = false;

    public void Update()
    {
        if(startGrounding == true)
        {
            groundingTime -= Time.deltaTime;
            if (groundingTime <= 0.0f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            startGrounding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            startGrounding = false;
            groundingTime = 1.0f;
        }
    }
}
