using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("Reference Scripts")]
    [Tooltip("PlayerInputEmitter sends info to this script")]
    [SerializeField] private PlayerInputEmitter inputEmitter;
    
    [Header("Reference Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    
    [Header("Variables")]
    [SerializeField] private float velocity = 5.0f;
    private Vector3 movementVector;
    private Vector3 rotationVector;

    //Animations Hashes
    private int velocityHash;
    private int rotationHash;

    private void Awake()
    {
        inputEmitter = GetComponent<PlayerInputEmitter>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        velocityHash = Animator.StringToHash("Velocity");
        rotationHash = Animator.StringToHash("Rotation");
    }

    void Update()
    {
        //Sets the movementVector according to the input
        movementVector = inputEmitter.Movement;
        movementVector.x = 0.0f;
        movementVector.y = 0.0f;
        //Sets the rotationVector according to the input
        rotationVector.y = inputEmitter.Movement.x;
        rotationVector.z = 0.0f;
        rotationVector.x = 0.0f;

        animator.SetFloat(velocityHash, movementVector.z);
        animator.SetFloat(rotationHash, rotationVector.y);
    }
    void FixedUpdate()
    {
        transform.Translate(movementVector * velocity * Time.deltaTime);
        transform.Rotate(rotationVector);
    }
}
