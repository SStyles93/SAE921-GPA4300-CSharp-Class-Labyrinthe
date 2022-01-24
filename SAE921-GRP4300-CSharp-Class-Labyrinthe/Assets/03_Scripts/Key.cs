using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Reference GameObjects")]
    [Tooltip("Canvas contained in the Key")]
    [SerializeField] private Canvas canvas;
    
    private float groundingTime = 1.0f;
    private bool startGrounding = false;


    private void Update()
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
    public void ActivateText()
    {
        canvas.gameObject.SetActive(true);
    }
    public void DeactivateText()
    {
        canvas.gameObject.SetActive(false);
    }
}
