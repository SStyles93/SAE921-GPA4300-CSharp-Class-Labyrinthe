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

    [SerializeField] private float timer = 15.0f;

    //Lets the object fall
    private float groundingTime = 1.0f;
    private bool startGrounding = false;

    //Checks if the key is held or not
    private bool isHeld = false;
    private bool isSet = false;
    private bool timerInitiated = false;

    private Vector3 initialPos;

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

    private void Start()
    {
        initialPos = transform.position;
    }

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

    public IEnumerator TimerUntilReset()
    {
        if (timerInitiated)
            yield break;

        timerInitiated = true;

        yield return new WaitForSecondsRealtime(timer);

        if (!isSet)
        {
            timerInitiated = false;
            ResetKey();
        }
    }
    
    private void ResetKey()
    {
        transform.position = initialPos;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerAction>().LetGo();
        GetComponent<AudioSource>().Play();
    }
}
