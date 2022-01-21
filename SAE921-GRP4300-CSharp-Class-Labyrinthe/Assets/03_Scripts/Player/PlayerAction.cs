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

    //Has to be set to false (true is for test purpose)
    [SerializeField] private bool canPickUp = true;

    //Animations Hashes
    private int isHoldingHash;
    private int pickingUpHash;

    private void Awake()
    {
        inputEmitter = GetComponent<PlayerInputEmitter>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        isHoldingHash = Animator.StringToHash("isHolding");
        pickingUpHash = Animator.StringToHash("PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEmitter.Action && canPickUp)
        {
            //Checks if player is already holding an object
            if (animator.GetBool(isHoldingHash))
            {
                //In case he is, let's go of the object
                LetGo();
            }
            else
            {
                //Otherwise, picks up the Object
                PickUp();
            }
            inputEmitter.Action = false;
        }
    }
    public void PickUp()
    {
        animator.SetTrigger(pickingUpHash);
        Hold();
        canPickUp = false;
    }
    public void Hold()
    {
        animator.SetBool(isHoldingHash, true);
    }
    public void LetGo()
    {
        animator.SetBool(isHoldingHash, false);
    }
}
