using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEmitter : MonoBehaviour
{

    [SerializeField] private Vector3 movement;
    private bool action;

    #region Getter/Setter

    public Vector3 Movement
    {
        get { return movement; }
        set { movement = value; }
    }
    public bool Action
    {
        get { return action; }
        set { action = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }
    public void OnMove(InputValue value)
    {
        //Default version
        movement.x = value.Get<Vector2>().x;
        movement.z = value.Get<Vector2>().y;
    }
    public void OnAction(InputValue value)
    {
        action = true;
    }
}
