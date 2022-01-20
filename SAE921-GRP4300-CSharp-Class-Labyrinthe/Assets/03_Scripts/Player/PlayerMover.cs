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
    [SerializeField] private float velocity;
    private Vector3 pointToLookAt;

    private void Awake()
    {
        inputEmitter = GetComponent<PlayerInputEmitter>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        pointToLookAt = transform.position + inputEmitter.Movement;
        transform.LookAt(pointToLookAt);
    }
    void FixedUpdate()
    {
        MovePlayer(inputEmitter.Movement);
    }
    void MovePlayer(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * velocity * Time.deltaTime));

    }
}
