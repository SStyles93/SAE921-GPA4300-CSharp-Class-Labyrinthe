using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Reference Scripts")]
    [Tooltip("PlayerInputEmitter sends info to this script")]
    [SerializeField] private PlayerInputEmitter inputEmitter;

    [Header("Reference Components")]
    [SerializeField] private Animator animator;

    [Header("Reference GameObjects")]
    [Tooltip("handPosition uses it's position to set objects to it")]
    [SerializeField] private GameObject handPosition;
    [SerializeField] private Key key;

    //Has to be set to false (true is for test purpose)
    [SerializeField] private bool canPickUp = false;

    //Animations Hashes
    private int isHoldingHash;
    private int pickingUpHash;

    private void Awake()
    {
        inputEmitter = GetComponent<PlayerInputEmitter>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        isHoldingHash = Animator.StringToHash("isHolding");
        pickingUpHash = Animator.StringToHash("PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEmitter.Action)
        {
            //Checks if player is already holding an object
            if (animator.GetBool(isHoldingHash))
            {
                //In case he is, let's go of the object
                LetGo();
            }
            else if(canPickUp)
            {
                //Otherwise, picks up the Object
                PickUp();
                //Note: The Animator will launch "Hold" after the PickUp anim !
            }
            inputEmitter.Action = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (handPosition.transform.childCount != 0)
            return;

        Key tmpKey = other.GetComponent<Key>();
        if (tmpKey)
        {
            tmpKey.ActivateText();

            if (!tmpKey.IsSet)
            {
                key = tmpKey;
                canPickUp = true;
            } 
        }
    }
    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Key>())
        {
            canPickUp = false;

            if(key != null && !key.IsHeld)
            {
                key = null;
            }

            other.GetComponent<Key>().DeactivateText();
        }
    }

    public void PickUp()
    {
        animator.SetTrigger(pickingUpHash);
        canPickUp = false;
    }

    public void Hold()
    {
        //Plays the Hold anim
        animator.SetBool(isHoldingHash, true);
        
        //Sets a key-hand binding
        key.transform.SetParent(handPosition.transform);
        key.transform.SetPositionAndRotation(
            handPosition.transform.position,
            handPosition.transform.rotation);
        key.GetComponent<Rigidbody>().detectCollisions = false;
        key.IsHeld = true;

        StartCoroutine(key.TimerUntilReset());
    }

    public void LetGo()
    {
        //stops the holding anim 
        animator.SetBool(isHoldingHash, false);

        //Cancels the key - hand binding
        if(key == null)
        {
            key = handPosition.GetComponentInChildren<Key>();
        }
        key.transform.SetParent(null);
        Rigidbody keyRigidBody = key.GetComponent<Rigidbody>();
        keyRigidBody.detectCollisions = true;
        keyRigidBody.isKinematic = false;
        key.IsHeld = false;

        key = null;
    }
}
