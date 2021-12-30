using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public MoveLogicController movementController;
    private float movement;
    private bool crouch;
    private bool jump;

    private void Start()
    {
        movementController = GetComponent<MoveLogicController>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        crouch = Input.GetButton("Crouch");
        jump = Input.GetButtonDown("Jump");

        movementController.Move(movement, crouch, jump);
    }
}
