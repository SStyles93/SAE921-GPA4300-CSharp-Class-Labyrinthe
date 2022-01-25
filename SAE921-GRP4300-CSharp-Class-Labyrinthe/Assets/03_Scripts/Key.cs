using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("Reference GameObjects")]
    [Tooltip("Canvas contained in the Key")]
    [SerializeField] private Canvas canvas;

    public enum KeyTypes{Cube, Sphere, Pyramid}
    [Space(20)]
    [SerializeField] private KeyTypes keyType;

    //Lets the object fall
    private float groundingTime = 1.0f;
    private bool startGrounding = false;

    //Checks if the key is held or not
    private bool isHeld = false;
    private bool isSet = false;

    #region GETTER/SETTER

    public bool IsHeld
    {
        get { return isHeld; }
        set { isHeld = value; }
    }
    public bool IsSet
    {
        get { return isSet; }
        set { isSet = value; }
    }
    public KeyTypes KeyType
    {
        get { return keyType; }
    }

    #endregion

    private void Update()
    {
        if (isSet)
            return;

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
        if (isSet)
            return;

        canvas.gameObject.SetActive(true);
    }
    public void DeactivateText()
    {
        if (isSet)
            return;

        canvas.gameObject.SetActive(false);
    }
    //public void DeleteText()
    //{
    //    if(canvas.GetComponent<Image>().gameObject.activeInHierarchy)
    //    canvas.GetComponentInChildren<Image>().gameObject.SetActive(false);
    //}
}
